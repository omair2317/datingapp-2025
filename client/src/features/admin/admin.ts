import { NgIf } from '@angular/common';
import { Component, inject } from '@angular/core';
import { AccountService } from '../../core/services/account-service';
import { UserManagement } from "./user-management/user-management";
import { PhotoManaement } from "./photo-manaement/photo-manaement";

@Component({
  selector: 'app-admin',
  imports: [UserManagement, PhotoManaement],
  templateUrl: './admin.html',
  styleUrl: './admin.css'
})
export class Admin {
  protected accountService = inject(AccountService);
  activeTab = 'photos';
  tabs =[
    {label: 'Photo moderation', value: 'photos'},
    {label: 'User management', value: 'roles'},
  ]

  setTab(tab: string) {
    this.activeTab = tab;
  }
}
