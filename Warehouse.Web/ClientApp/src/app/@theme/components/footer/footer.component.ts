import { Component } from '@angular/core';

@Component({
  selector: 'ngx-footer',
  styleUrls: ['./footer.component.scss'],
  template: `
    <span class="created-by">Created by Murat ÖNER - 2019</span>
    <div class="socials">
      <a href="https://github.com/muratoner" target="_blank" class="ion ion-social-github"></a>
      <a href="https://twitter.com/muratonerr" target="_blank" class="ion ion-social-twitter"></a>
      <a href="https://www.linkedin.com/in/muratoner" target="_blank" class="ion ion-social-linkedin"></a>
    </div>
  `,
})
export class FooterComponent {
}
