import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-fetch-data',
    templateUrl: './fetch-data.component.html',
    styleUrls: ['./fetch-data.component.css']
})
    /*
     * TODO : 1.)Write an http calling service and inject in constructor
     *        2.)Clear all inputs after each command click
     * */
export class FetchDataComponent {

    //Exposed values on HTML template:
    public money: string;
    public commandResult: string = "Soda Machine started";
    public sodas: any;

    //private global instances.  
    private _http: HttpClient;
    private _baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this._baseUrl = baseUrl;
        this._http = http;
        
        this._http.get(baseUrl + 'api/Order/GetSodaInventory').subscribe(
            result => {
                this.sodas = result;
                this.getAvailableMoney();
            },
            error => {
                alert("Error reading sodas in machine");
                console.error(error);
            });  
    }

    insert(insertedMoney: number) {

        this._http.post(this._baseUrl + 'api/Money/Insert', insertedMoney).subscribe(
            result => {
                this.money = result.toString();
                this.commandResult = "Inserted money in the machine";
            },
            error => {
                this.commandResult = "Error inserting money in the machine";
                console.error(error);
            });
    }

    recall() {
        
        this._http.get(this._baseUrl + 'api/Money/Recall').subscribe(
            result => {
                this.commandResult = "Returning " + result.toString() + " to customer";
                this.getAvailableMoney()
            },
            error => {
                this.commandResult = "Error giving money back";
                console.error(error);
            });
    }

    order(orderedSodaId: string) {

        var sodaId = +orderedSodaId;
        if (isNaN(sodaId)) {
            alert('Error in soda inventory');
        }
        else {
            this._http.post(this._baseUrl + 'api/Order/ProcessOrder', sodaId).subscribe(
                result => {
                    this.commandResult = result.toString();
                    this.getAvailableMoney()
                },
                error => {
                    this.commandResult = "Error processing the order";
                    console.error(error);
                });
        }
    }

    smsOrder(orderedSodaId: string) {
        var sodaId = +orderedSodaId;
        if (isNaN(sodaId)) {
            alert('Error in soda inventory');
        }
        else {
            this._http.post(this._baseUrl + 'api/Order/SmsOrder', sodaId).subscribe(
                result => { this.commandResult = result.toString(); },
                error => {
                    this.commandResult = "Error processing the order";
                    console.error(error);
                });
        }
    }

    private getAvailableMoney() {
        
        this._http.get(this._baseUrl + 'api/Money/Get').subscribe(
            result => { this.money = result.toString() },
            error => {
                alert("Error getting available money");
                console.error(error);
            });
    }
}
