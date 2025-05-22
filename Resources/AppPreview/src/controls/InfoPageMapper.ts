import { CtaComponent } from "../components/CtaComponent";
import { TileComponent } from "../components/TileComponent";
import { Content } from "../interfaces/Content";
import { Cta } from "../interfaces/Cta";
import { PageInfoContentStructure } from "../interfaces/PageInfoContentStructure";
import { Page } from "../interfaces/Page";
import { Row } from "../interfaces/Row";
import { Tile } from "../interfaces/Tile";
import { InfoType } from "../interfaces/InfoType";

export class InfoPageMapper {
    pageData: PageInfoContentStructure;
    pageId: string;

    constructor(page: Page) {
        this.pageId = page.PageId;
        this.pageData = page.PageInfoStructure;
    }

    private renderImage(content: InfoType) {
        const imageContainer = document.createElement('div');
        imageContainer.classList.add('tbap-image-container');

        const image = document.createElement('img');
        if (content?.InfoValue) {
            image.src = content?.InfoValue;
            imageContainer.appendChild(image);
        } 

        return imageContainer;
    }

    private renderDescription(content: InfoType) {
        const description = document.createElement('div');
        description.classList.add('tbap-description-container');
        if (content?.InfoValue) {
            description.innerHTML = content?.InfoValue;
        }
        return description;
    }

    private renderTileRow(row: InfoType): HTMLElement {
        // const isHighPriorityRow = false;
        // const hasSingleTile = row.Tiles.length === 1;
        const hasSingleTile = row.Tiles?.length === 1;
        // const isHighPriorityRow = isFirstRow && hasSingleTile;
        const isHighPriorityRow = hasSingleTile;

        // Create a row container
        const rowElement = document.createElement('div');
        rowElement.className = 'tbap-row';
        rowElement.id = row.InfoId;
        if (row.Tiles) {
            const rowTileLength = row.Tiles.length;
            row?.Tiles.forEach((tile, index) => {
                // const isHighPriority = isHighPriorityRow && index === 0;
                const isHighPriority = isHighPriorityRow;
                const tileComponent = new TileComponent(tile, isHighPriority, this.pageId, rowTileLength);
                rowElement.appendChild(tileComponent.getElement());
            });
        }       
        
        return rowElement;
    }

    private renderCta(content: InfoType): HTMLElement | null {
        const ctaContainer = document.createElement('div');
        ctaContainer.className = 'tbap-cta-container';
        if (!content.CtaAttributes) return null;
        const ctaElement = new CtaComponent(content.CtaAttributes);
        const ctaButton = ctaElement.getCta();
        ctaButton.addEventListener('click', () => {
            ctaElement.handleCtaClick(content.CtaAttributes);
        })
        ctaContainer.appendChild(ctaButton);
        return ctaContainer;
    }
    renderContent(container: HTMLElement): void {
        if (!this.pageData?.InfoContent?.length) {
            const emptyContent = document.createElement('div');
            emptyContent.className = 'tbap-empty';
            emptyContent.innerText = 'No content available';
            container.appendChild(emptyContent);
            return;
        }

        const columnElement = document.createElement('div');
        columnElement.className = 'tbap-content-column';
        
        this.pageData.InfoContent.forEach((content: InfoType) => {
            let contentEl: HTMLElement | null = null;
            if (content.InfoType === "Image" && content.InfoValue) {
                contentEl = this.renderImage(content);
            } else  if(content.InfoType === "Description" && content.InfoValue) {
                contentEl = this.renderDescription(content);
            } else if(content.InfoType === "Cta" && content.CtaAttributes) {
                contentEl = this.renderCta(content);
                // columnElement.appendChild(contentEl);
            } else if (content.InfoType === "TileRow" && content.Tiles?.length) {
                const rowElement = this.renderTileRow(content);
                columnElement.appendChild(rowElement);
            }

            if (contentEl) {
                columnElement.appendChild(contentEl);
            }            
        });
        
        container.appendChild(columnElement);
    }
}