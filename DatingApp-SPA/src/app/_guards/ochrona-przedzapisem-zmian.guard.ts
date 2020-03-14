import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';


@Injectable()
export class OchronaPrzedzapisemZmian implements CanDeactivate<MemberEditComponent> {
    canDeactivate(component: MemberEditComponent) {
        if (component.editForm.dirty) {
            return confirm('Jesteś pewien, że chcesz kontynuować? Wszystkie niezapisane zmiany zostaną utracone');
        }
        return true;
    }
}