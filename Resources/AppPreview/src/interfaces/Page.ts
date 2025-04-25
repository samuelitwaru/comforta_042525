import { PageContentStructure } from "./PageContentStructure";
import { PageMenuStructure } from "./PageMenuStructure";

export interface Page {
    PageId: string;
    PageName: string;
    PageType: string;
    PageStructure: string;
    PageMenuStructure: PageMenuStructure;
    PageContentStructure: PageContentStructure;
}