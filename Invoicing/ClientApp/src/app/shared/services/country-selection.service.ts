import { Injectable } from "@angular/core";
import { Country } from "../../common/model/country.model";
import { DataService } from "./data.service";

@Injectable()
export class CountrySelectionService extends DataService<Country> {

}
