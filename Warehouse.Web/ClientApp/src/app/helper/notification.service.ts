import { Injectable } from '@angular/core'
import { Localization } from './localization'

declare var Notiflix: Notiflix

@Injectable()
export class NotificationService {

  constructor() {
    Notiflix.Notify.Init({
      timeout: 5000
    })

    Notiflix.Report.Init({})

    Notiflix.Loading.Init({
      svgColor: '#3366ff'
    })

    Notiflix.Confirm.Init({
      okButtonColor: '#fff',
      titleColor: '#3366ff',
      okButtonBackground: '#3366ff'
    })
  }

  private activeBtnClose() {
    Notiflix.Notify.Merge({
      closeButton: true
    })
  }

  private passiveBtnClose() {
    Notiflix.Notify.Merge({
      closeButton: false
    })
  }

  confirm(title: string, message: string, callbackYes?: Action) {
    Notiflix.Confirm.Show(
      title,
      message,
      Localization.button.yes,
      Localization.button.no,
      callbackYes
    )
  }

  confirmDelete(callbackYes?: Action) {
    Notiflix.Confirm.Show(
      Localization.dialog.titleDelete,
      Localization.dialog.messageDeleteAreYouSure,
      Localization.button.yes,
      Localization.button.no,
      callbackYes
    )
 }

  notifySuccess(title: string, callbackDone?: Action) {
    this .passiveBtnClose()
    Notiflix.Notify.Success(title, callbackDone)
  }

  notifyInfo(title: string, callbackDone?: Action) {
    this .passiveBtnClose()
    Notiflix.Notify.Info(title, callbackDone)
  }

  notifyWarning(title: string, callbackDone?: Action) {
    this .passiveBtnClose()
    Notiflix.Notify.Warning(title, callbackDone)
  }

  notifyDanger(title: string, callbackDone?: Action) {
    this .activeBtnClose()
    Notiflix.Notify.Failure(title, callbackDone)
  }

  dialogSuccess(title: string, message: string, callbackClick?: Action) {
    Notiflix.Report.Success(title, message, Localization.button.ok, callbackClick)
  }

  dialogInfo(title: string, message: string, callbackClick?: Action) {
    Notiflix.Report.Info(title, message, Localization.button.ok, callbackClick)
  }

  dialogWarning(title: string, message: string, callbackClick: Action) {
    Notiflix.Report.Warning(title, message, Localization.button.ok, callbackClick)
  }

  dialogDanger(title: string, message: string | string[], callbackClick?: Action) {
    const type: any = typeof(message)
    const res: any = type === 'Array' ? (message as string[]).join('<br/>') : message
    Notiflix.Report.Failure(title, res, Localization.button.ok, callbackClick)
  }

  loadingShow(title?: string) {
    Notiflix.Loading.Hourglass(title)
  }

  loadingHide() {
    Notiflix.Loading.Remove()
  }
}

interface Notiflix {
  Notify: {
    Init(obj: Object)
    Merge(obj: Object)
    Failure(title?: string, callbackClick?: Action)
    Warning(title?: string, callbackClick?: Action)
    Info(title?: string, callbackClick?: Action)
    Success(title?: string, callbackClick?: Action)
  }

  Report: {
    Init(obj: Object)

    Failure(
      title?: string,
      content?: string,
      btnText?: string,
      callbackClick?: Action
    )

    Warning(
      title?: string,
      content?: string,
      btnText?: string,
      callbackClick?: Action
    )

    Info(
      title?: string,
      content?: string,
      btnText?: string,
      callbackClick?: Action
    )

    Success(
      title?: string,
      content?: string,
      btnText?: string,
      callbackClick?: Action
    )
  }

  Confirm: {
    Init(obj: Object)
    Show(
      title?: string,
      message?: string,
      yesBtnText?: string,
      noBtnText?: string,
      callbackYes?: Action
    )
  }

  Loading: {
    Init(obj: InitLoading)
    Standart(text?: string)
    Hourglass(text?: string)
    Circle(text?: string)
    Arrows(text?: string)
    Pulse(text?: string)
    Dots(text?: string)
    Remove()
  }
}

interface InitLoading {
  svgColor?: string
  messageColor?: string
  backgroundColor?: string
  useGoogleFont?: boolean
  svgSize?: string
  customSvgUrl?: string
  messageFontSize?: string
  fontFamily?: string

}

declare type Action = () => void;
