import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from './sidebar/sidebar.component';
import { FooterComponent } from './footer/footer.component';
import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule } from '@angular/router';
import { BackButtonComponent } from './back-button/back-button.component';
import { NavbarModule, WavesModule, DropdownModule, ButtonsModule } from 'angular-bootstrap-md';

@NgModule({
    declarations: [
        FooterComponent,
        NavbarComponent,
        SidebarComponent,
        BackButtonComponent
    ],
    imports: [
        CommonModule,
        RouterModule,
        NavbarModule,
        ButtonsModule,
        DropdownModule.forRoot(),
        WavesModule.forRoot()

    ],
    exports: [FooterComponent,
        NavbarComponent,
        SidebarComponent,
        BackButtonComponent]
})
export class CoreModule { }
