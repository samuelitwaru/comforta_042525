import { CallToAction } from "../../interfaces/CallToAction";
import { CtaAttributes } from "../../interfaces/CtaAttributes";
import { InfoType } from "../../interfaces/InfoType";
import { ThemeCtaColor } from "../../models/Theme";
import { ctaTileDEfaultAttributes, DefaultAttributes, tileDefaultAttributes } from "../../utils/default-attributes";
import { randomIdGenerator } from "../../utils/helpers";
import { ContentMapper } from "../editor/ContentMapper";
import { CtaButtonProperties } from "../editor/CtaButtonProperties";
import { InfoSectionController } from "../InfoSectionController";
import { CtaSvgManager } from "./CtaSvgManager";
import { ThemeManager } from "./ThemeManager";

export class CtaManager {
    private editor: any;
    private contentMapper: ContentMapper;
    private themeManager: ThemeManager;
    private ctaSvgManager: CtaSvgManager;
    private pageId: string;
    private pageData: any;

    constructor() {
        this.editor = (globalThis as any).activeEditor;
        this.pageId = (globalThis as any).currentPageId;
        this.pageData = (globalThis as any).pageData;
        this.contentMapper = new ContentMapper(this.pageId);
        this.themeManager = new ThemeManager();
        this.ctaSvgManager = new CtaSvgManager();
    }

    addCtaButton(ctaButton: CallToAction): void {
        const ctaContainer = this.editor.Components.getWrapper().find(".cta-button-container")[0];
        if (!ctaContainer) return;
        
        const buttonId = randomIdGenerator(12);
        const {ctaButtonEl, ctaAction} = this.getIconAndAction(ctaButton, buttonId);
        
        const ctaMapper = {
            CtaId: buttonId,
            CtaLabel: ctaButton.CallToActionName,
            CtaType: ctaButton.CallToActionType,
            CtaButtonType: "Round",
            CtaBGColor: "CtaColorOne",
            CtaAction: ctaAction
        };
                
        ctaContainer.append(ctaButtonEl);
        this.contentMapper.addContentCta(ctaMapper);
    }

    checkIfExisting(ctaButtonEl: any): boolean {
        const tempElement = document.createElement('div');
        tempElement.innerHTML = ctaButtonEl;
        const buttonElement = tempElement.querySelector('[button-type]');
        
        if (!buttonElement) {
            return false;
        }
        
        const buttonType = buttonElement.getAttribute('button-type');
        const ctaButtons = this.editor.Components.getWrapper().getEl().querySelectorAll(".cta-button-container [button-type]");
        
        return Array.from(ctaButtons).some((ctaButton: any) => {
            return ctaButton.getAttribute("button-type") === buttonType;
        });
    }

    changeCtaColor(color: any): void {
        const selectedComponent = (globalThis as any).selectedComponent;
        if (!selectedComponent) return;

        const ctaButton = selectedComponent.find(".cta-styled-btn")[0];
        if (!ctaButton) return;
        
        ctaButton.addStyle({
            "background-color": color.CtaColorCode,
        });

        ctaButton.getEl().style.backgroundColor = color.CtaColorCode;

        const ctaButtonComponent = ctaButton.parent();
        if (this.isInformationPage()) {
            const infoSectionController = new InfoSectionController();
            infoSectionController.updateInfoCtaAttributes(selectedComponent.getId(), 'CtaBGColor', color.CtaColorName);                
        } else {
            this.contentMapper.updateContentCtaBGColor(ctaButtonComponent.getId(), color.CtaColorName);
        }            
    }
 
    changeToPlainButton(): void {
        const selectedComponent = this.getSelectedComponent();
        if (!selectedComponent) return;
        
        const ctaButtonAttributes = this.getCtaButtonAttributes(selectedComponent);
        if (!ctaButtonAttributes) return;

        const plainButton = this.createPlainButtonHTML(selectedComponent.getId(), ctaButtonAttributes);
        this.selectComponentAfterAdd(selectedComponent.getId(), selectedComponent, plainButton);

        this.updateCtaButtonType(selectedComponent.getId(), 'FullWidth');
        this.updateProperties();
    }

    changeToIconButton(): void {
        const selectedComponent = this.getSelectedComponent();
        if (!selectedComponent) return;
        
        const ctaButtonAttributes = this.getCtaButtonAttributes(selectedComponent);
        if (!ctaButtonAttributes) return;

        const ctaSVG = this.ctaSvgManager.getTypeSVG(ctaButtonAttributes);
        if (!ctaSVG) return;
        const iconButton = this.createIconButtonHTML(selectedComponent.getId(), ctaButtonAttributes, ctaSVG);
        
        this.selectComponentAfterAdd(selectedComponent.getId(), selectedComponent, iconButton);
        this.updateCtaButtonType(selectedComponent.getId(), 'Icon');
        this.updateProperties();
    }

    changeToImgButton(): void {
        const selectedComponent = this.getSelectedComponent();
        if (!selectedComponent) return;
        
        const ctaButtonAttributes = this.getCtaButtonAttributes(selectedComponent);
        if (!ctaButtonAttributes) return;

        const imgButton = this.createImgButtonHTML(selectedComponent.getId(), ctaButtonAttributes);
        this.selectComponentAfterAdd(selectedComponent.getId(), selectedComponent, imgButton);
        
        this.updateCtaButtonType(selectedComponent.getId(), 'Image');
        
        if (!this.isInformationPage()) {
            const defaultImagePath = '/Resources/UCGrapes1/src/images/image.png';
            this.contentMapper.updateContentButtonType(
                ctaButtonAttributes.CtaId, 
                'Image', 
                defaultImagePath
            );
        }
        
        this.updateProperties();
    }

    changeToElipseButton(): void {
        const selectedComponent = this.getSelectedComponent();
        if (!selectedComponent) return;
        
        const ctaButtonAttributes = this.getCtaButtonAttributes(selectedComponent);
        if (!ctaButtonAttributes) return;

        const ctaSVG = this.ctaSvgManager.getTypeSVG(ctaButtonAttributes);
        if (!ctaSVG) return;
        const elipseButton = this.createElipseButtonHTML(selectedComponent.getId(), ctaButtonAttributes, ctaSVG);
        this.selectComponentAfterAdd(selectedComponent.getId(), selectedComponent, elipseButton);
        
        this.updateCtaButtonType(selectedComponent.getId(), 'Round');
        
        this.updateProperties();
    }

    removeCta(ctaBadge: HTMLElement): void {
        const ctaBadgeParent = ctaBadge.parentElement;
        if (!ctaBadgeParent?.id) return;
        
        const ctaBadgeParentComponent = this.editor.Components.getWrapper().find(
            "#" + ctaBadgeParent.id
        )[0];
        
        if (!ctaBadgeParentComponent) return;
        
        if (this.isInformationPage()) {
            const infoSectionController = new InfoSectionController();
            infoSectionController.deleteCtaButton(ctaBadgeParentComponent.parent().getId());
            return;
        }
        
        const ctaButtonComponent = ctaBadgeParentComponent.parent();
        if (ctaButtonComponent) {
            ctaButtonComponent.remove();
            this.contentMapper.removeContentCta(ctaButtonComponent.getId());
        }
    }
    
    getIconAndAction(ctaButton: any, id: string) {
        let ctaButtonEl;
        let ctaAction;
        const type = ctaButton?.CallToActionType;
        
        switch (type) {
            case "Phone":
                ctaButtonEl = this.ctaSvgManager.phoneCta(ctaButton, id);
                ctaAction = ctaButton?.CallToActionPhoneNumber;
                break;
            case "Email":
                ctaButtonEl = this.ctaSvgManager.emailCta(ctaButton, id);
                ctaAction = ctaButton?.CallToActionEmail;
                break;
            case "WebLink":
                ctaButtonEl = this.ctaSvgManager.urlCta(ctaButton, id);
                ctaAction = ctaButton?.CallToActionUrl;
                break;
            case "Form":
                ctaButtonEl = this.ctaSvgManager.formCta(ctaButton, id);
                ctaAction = ctaButton?.CallToActionUrl;
                break;
            default:
                break;
        } 
        
        return { ctaButtonEl, ctaAction };
    }

    // Private helper methods
    private getSelectedComponent(): any {
        return (globalThis as any).selectedComponent;
    }

    private isInformationPage(): boolean {
        return this.pageData.PageType === "Information";
    }

    private getCtaButtonAttributes(selectedComponent: any): any {
        if (this.isInformationPage()) {
            const tileInfoSectionAttributes: InfoType = (
                globalThis as any
            ).infoContentMapper.getInfoContent(selectedComponent.getId());
            return tileInfoSectionAttributes?.CtaAttributes;
        } 
        
        return this.contentMapper.getContentCta(selectedComponent.getId());
    }

    private updateCtaButtonType(componentId: string, buttonType: string): void {
        console.log('iconButton');
        if (this.isInformationPage()) {
            const infoSectionController = new InfoSectionController();
            infoSectionController.updateInfoCtaAttributes(componentId, 'CtaButtonType', buttonType); 
        } else {
            this.contentMapper.updateContentButtonType(this.getCtaButtonAttributes(this.getSelectedComponent()).CtaId, buttonType);
        }
    }

    private selectComponentAfterAdd(ctaId: string, selectedComponent: any, newComponent: any): void {
        this.editor.once("component:add", () => {
            const addedComponent = this.editor
              .getWrapper()
              .find(`#${ctaId}`)[0];
            if (addedComponent) {
                this.editor.select(addedComponent);
            }
        });
        
        selectedComponent.replaceWith(newComponent);
    }

    private updateProperties(): void {
        const selectedComponent = this.getSelectedComponent();
        const ctaButtonAttributes = this.getCtaButtonAttributes(selectedComponent);
        if (ctaButtonAttributes.CtaButtonIcon == "") ctaButtonAttributes.CtaButtonIcon = "Email"

        if (!ctaButtonAttributes || !selectedComponent) return;

        const ctaButtonProperties = new CtaButtonProperties(selectedComponent, ctaButtonAttributes);
        ctaButtonProperties.setctaAttributes();
    }

    private createPlainButtonHTML(componentId: string, attributes: any): string {
        const bgColor = this.themeManager.getThemeCtaColor(attributes.CtaBGColor);
        const textColor = attributes.CtaColor || "#ffffff";
        const pageTypeAttribute = this.isInformationPage() 
            ? `data-gjs-type="info-cta-section"` 
            : `data-gjs-type=cta-buttons`;

        return `
            <div id="${componentId}" 
                button-type="${attributes.CtaType}"
                class="plain-button-container"
                ${pageTypeAttribute}
                ${ctaTileDEfaultAttributes}>
                <button ${DefaultAttributes} class="plain-button cta-styled-btn"
                    style="background-color: ${bgColor}">
                    <div ${DefaultAttributes} id="ihd0f" class="cta-badge">
                            <i ${DefaultAttributes} id="i7o62" data-gjs-type="default" class="fa fa-minus"></i>
                    </div>
                    <span ${DefaultAttributes} class="label" style="color:${textColor}"> ${attributes.CtaLabel}</span> 
                </button>
            </div>
        `;
    }

    private createIconButtonHTML(componentId: string, attributes: any, ctaSVG: string): string {
        const bgColor = this.themeManager.getThemeCtaColor(attributes.CtaBGColor);
        const textColor = attributes.CtaColor || "#ffffff";
        const pageTypeAttribute = this.isInformationPage() 
            ? `data-gjs-type="info-cta-section"` 
            : `data-gjs-type=cta-buttons`;

        return `
            <div id="${componentId}" 
                button-type="${attributes.CtaType}"
                ${ctaTileDEfaultAttributes} 
                ${pageTypeAttribute}
                class="img-button-container">
                <div ${DefaultAttributes} class="img-button cta-styled-btn"
                    style="background-color: ${bgColor}">
                    <span ${DefaultAttributes} class="img-button-icon">
                        ${ctaSVG} 
                        <svg class="icon-edit-button" title="Change icon" ${DefaultAttributes} xmlns="http://www.w3.org/2000/svg" id="Component_57_1" data-name="Component 57 – 1" width="20" height="20" viewBox="0 0 33 33">
                            <g ${DefaultAttributes} id="Ellipse_532" data-name="Ellipse 532" fill="#fff" stroke="#5068a8" stroke-width="2">
                                <circle ${DefaultAttributes} cx="16.5" cy="16.5" r="16.5" stroke="none"/>
                                <circle ${DefaultAttributes} cx="16.5" cy="16.5" r="16" fill="none"/>
                            </g>
                            <path ${DefaultAttributes} id="Icon_feather-edit-2" data-name="Icon feather-edit-2" d="M12.834,3.8a1.854,1.854,0,0,1,2.622,2.622L6.606,15.274,3,16.257l.983-3.606Z" transform="translate(7 6.742)" fill="#5068a8" stroke="#5068a8" stroke-linecap="round" stroke-linejoin="round" stroke-width="1"/>
                        </svg>
                    </span>
                    <div${DefaultAttributes} class="cta-badge">
                        <i ${DefaultAttributes} class="fa fa-minus"></i>
                    </div>
                    <span ${DefaultAttributes} class="img-button-label label" style="color:${textColor}">${attributes.CtaLabel}</span>
                    <i ${DefaultAttributes} class="fa fa-angle-right img-button-arrow" style="color:${textColor}"></i>
                </div>
            </div>
        `;
    }

    private createImgButtonHTML(componentId: string, attributes: any): string {
        const bgColor = this.themeManager.getThemeCtaColor(attributes.CtaBGColor);
        const textColor = attributes.CtaColor || "#ffffff";
        const pageTypeAttribute = this.isInformationPage() 
            ? `data-gjs-type="info-cta-section"` 
            : `data-gjs-type=cta-buttons`;
        const imgUrl = attributes.CtaButtonImgUrl || `/Resources/UCGrapes1/src/images/image.png`;
        
        const editIconSVG = attributes.CtaButtonImgUrl 
            ? `<svg ${DefaultAttributes} xmlns="http://www.w3.org/2000/svg" id="Component_57_1" data-name="Component 57 – 1" width="22" height="22" viewBox="0 0 33 33">
                <g ${DefaultAttributes} id="Ellipse_532" data-name="Ellipse 532" fill="#fff" stroke="#5068a8" stroke-width="2">
                    <circle ${DefaultAttributes} cx="16.5" cy="16.5" r="16.5" stroke="none"/>
                    <circle ${DefaultAttributes} cx="16.5" cy="16.5" r="16" fill="none"/>
                </g>
                <path ${DefaultAttributes} id="Icon_feather-edit-2" data-name="Icon feather-edit-2" d="M12.834,3.8a1.854,1.854,0,0,1,2.622,2.622L6.606,15.274,3,16.257l.983-3.606Z" transform="translate(7 6.742)" fill="#5068a8" stroke="#5068a8" stroke-linecap="round" stroke-linejoin="round" stroke-width="1"/>
              </svg>`
            : `<svg ${DefaultAttributes} xmlns="http://www.w3.org/2000/svg" id="Component_53_4" data-name="Component 53 – 4" width="22" height="22" viewBox="0 0 22 22">
                <g ${DefaultAttributes} id="Group_2309" data-name="Group 2309">
                    <g ${DefaultAttributes} id="Group_2307" data-name="Group 2307">
                    <g ${DefaultAttributes} id="Ellipse_6" data-name="Ellipse 6" fill="#fdfdfd" stroke="#5068a8" stroke-width="1">
                        <circle ${DefaultAttributes} cx="11" cy="11" r="11" stroke="none"/>
                        <circle ${DefaultAttributes} cx="11" cy="11" r="10.5" fill="none"/>
                    </g>
                    </g>
                </g>
                <path ${DefaultAttributes} id="Icon_ionic-ios-add" data-name="Icon ionic-ios-add" d="M18.342,13.342H14.587V9.587a.623.623,0,1,0-1.245,0v3.755H9.587a.623.623,0,0,0,0,1.245h3.755v3.755a.623.623,0,1,0,1.245,0V14.587h3.755a.623.623,0,1,0,0-1.245Z" transform="translate(-2.965 -2.965)" fill="#5068a8"/>
              </svg>`;

        return `
            <div id="${componentId}" 
                button-type="${attributes.CtaType}"
                ${ctaTileDEfaultAttributes} 
                ${pageTypeAttribute}
                class="img-button-container">
                <div ${DefaultAttributes} class="img-button cta-styled-btn"
                    style="background-color: ${bgColor}">
                    <span ${DefaultAttributes} class="img-button-section">
                        <img ${DefaultAttributes} src="${imgUrl}" />
                        <span ${DefaultAttributes} class="edit-cta-image">
                            ${editIconSVG}
                        </span>
                    </span>
                    <div${DefaultAttributes} class="cta-badge">
                        <i ${DefaultAttributes} class="fa fa-minus"></i>
                    </div>
                    <span ${DefaultAttributes} class="img-button-label label" style="color:${textColor}">${attributes.CtaLabel}</span>
                    <i ${DefaultAttributes} class="fa fa-angle-right img-button-arrow" style="color:${textColor}"></i>
                </div>
            </div>
        `;
    }

    private createElipseButtonHTML(componentId: string, attributes: any, ctaSVG: string): string {
        const bgColor = this.themeManager.getThemeCtaColor(attributes.CtaBGColor);
        const textColor = attributes.CtaColor || "#ffffff";
        const pageTypeAttribute = this.isInformationPage() 
            ? `data-gjs-type="info-cta-section"` 
            : `data-gjs-type=cta-buttons`;
        return `
        <div ${ctaTileDEfaultAttributes} ${pageTypeAttribute}
          data-gjs-type="cta-buttons" 
          button-type="${attributes.CtaType}" 
          class="cta-container-child cta-child"
          id="${componentId}">              
            <div class="cta-button cta-styled-btn" ${DefaultAttributes}
              style="background-color: ${bgColor}">
                ${ctaSVG}
                <div class="cta-badge" ${DefaultAttributes}><i ${DefaultAttributes} data-gjs-type="default" class="fa fa-minus"></i></div>
            </div>
            <span class="cta-label label" ${DefaultAttributes}>${attributes.CtaLabel}</span>
        </div>
        `;
    }

    public changeCtaButtonIcon(icon: any, ctaId: string, ctaAttributes: CtaAttributes): void {
        if (ctaAttributes && ctaId) {
            if (this.isInformationPage()) {
                const selectedComponent = this.getSelectedComponent();
                if (selectedComponent) {
                    const iconComponent = selectedComponent.find(".img-button-icon")[0];
                    if (iconComponent) {
                        const tempElement = document.createElement('div');
                        tempElement.innerHTML = icon.svg;
                        const newSvgElement = tempElement.querySelector('svg');
                        
                        if (newSvgElement) {
                            newSvgElement.setAttribute('height', '32');
                            newSvgElement.setAttribute('width', '32');
                            
                            const pathElements = newSvgElement.querySelectorAll('path');
                            pathElements.forEach(path => {
                                path.setAttribute('fill',  ctaAttributes?.CtaColor || '#fff');
                            });
                            
                            const updatedSvg = tempElement.innerHTML;
                            
                            const svgComponent = iconComponent.find("svg")[0];
                            if (svgComponent) {
                                svgComponent.replaceWith(updatedSvg);
                            }
                            this.updateCtaIconMapper(ctaId, icon);
                        }
                    }
                }
            }
        }
    }

    private updateCtaIconMapper(ctaId: string, icon: any): void {
        if (this.isInformationPage()) {
            const selectedComponent = this.getSelectedComponent();
            if (selectedComponent) {
                const infoSectionController = new InfoSectionController();
                infoSectionController.updateInfoCtaAttributes(ctaId, 'CtaButtonIcon', icon.name);
            }
        }
    }
}