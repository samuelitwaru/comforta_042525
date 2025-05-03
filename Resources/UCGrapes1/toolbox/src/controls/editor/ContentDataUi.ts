import { Modal } from "../../ui/components/Modal";
import Quill from "quill";
import { ContentDataManager } from "./ContentDataManager";
import { ImageUpload } from "../../ui/components/tools-section/tile-image/ImageUpload";
import { ConfirmationBox } from "../../ui/components/ConfirmationBox";
import { InfoSectionController } from "../InfoSectionController";
import { CtaIconsListPopup } from "../../ui/views/CtaIconsListPopup";
import { i18n } from "../../i18n/i18n";

export class ContentDataUi {
    e: any;
    editor: any;
    page: any;
    contentDataManager: any;
    infoSectionController: any;

    constructor(e: any, editor: any, page: any) {
        this.e = e;
        this.editor = editor;
        this.page = page;
        this.infoSectionController = new InfoSectionController();
        this.contentDataManager = new ContentDataManager(this.editor, this.page);
        this.init();
    }

    private init() {
        this.openContentEditModal();
        this.openImageEditModal();
        this.openDeleteModal();
        this.updateCtaButtonImage();
        this.updateCtaButtonIcon();
    }

    private openContentEditModal() {
        if ((this.e.target as Element).closest('.tb-edit-content-icon')) {
            const modalBody = document.createElement('div');
            const infoDescSection = this.e.target.closest('[data-gjs-type="info-desc-section"].info-desc-section');
            console.log("infoDescSection", infoDescSection);
            const modalContent = document.createElement('div');
            modalContent.id = 'editor';
            modalContent.innerHTML = `${this.getDescription()}`;

            const submitSection = document.createElement('div');
            submitSection.classList.add('popup-footer');
            submitSection.style.marginBottom = '-12px';

            const saveBtn = this.createButton('submit_form', 'tb-btn-primary', i18n.t("tile.save_button"));
            const cancelBtn = this.createButton('cancel_form', 'tb-btn-outline', i18n.t("tile.cancel_button"));


            submitSection.appendChild(saveBtn);
            submitSection.appendChild(cancelBtn);

            modalBody.appendChild(modalContent);
            modalBody.appendChild(submitSection);

            const modal = new Modal({
                title: i18n.t("tile.edit_content"),
                width: "500px",
                body: modalBody
            });
            modal.open();

            const quill = new Quill("#editor", {
                modules: {
                    toolbar: [
                    ["bold", "italic", "underline", "link"],
                    [{ list: "ordered" }, { list: "bullet" }],
                    ],
                },
                theme: "snow",
            });
           
            saveBtn.addEventListener('click', () => {
                const content = document.querySelector("#editor .ql-editor") as HTMLElement;
                const correctedContent = this.correctULTagFromQuill(content.innerHTML);
            
                if (this.page.PageType === "Information" && infoDescSection) {
                    this.infoSectionController.updateDescription(correctedContent, infoDescSection.id);
                    modal.close();
                    return;
                }
                this.contentDataManager.saveContentDescription(correctedContent);
                modal.close();
            });
        }
    }

    private openImageEditModal() {
        if ((this.e.target as Element).closest('.tb-edit-image-icon')) {
            const image = this.e.target.closest('[data-gjs-type="info-image-section"].info-image-section');

            const modal = document.createElement("div");
            modal.classList.add("tb-modal");
            modal.style.display = "flex";
            const type = this.page.PageType === "Information" ? "info" : "content";
            const modalContent = new ImageUpload(type, (image as HTMLElement)?.id);
            modalContent.render(modal);
    
            const uploadInput = document.createElement("input");
            uploadInput.type = "file";
            uploadInput.multiple = true;
            uploadInput.accept = "image/jpeg, image/jpg, image/png";
            uploadInput.id = "fileInput";
            uploadInput.style.display = "none";
    
            document.body.appendChild(modal);
            document.body.appendChild(uploadInput);
        }
    }

    private openDeleteModal() {
        if ((this.e.target as Element).closest('.tb-delete-image-icon')) {
            if (this.page.PageType === "Location" || this.page.PageType === "Reception") {
                return;
            }
    
            const title = this.page.PageType === "Information" ? "Delete Description" : "Delete Image";
            const message = "Are you sure you want to delete this section?";
    
            const handleConfirmation = async () => {
                if (this.page.PageType === "Information") {
                    const targetElement = (this.e.target as Element);
                    const infoSection = targetElement.closest('[data-gjs-type="info-desc-section"].info-desc-section') ||
                                         targetElement.closest('[data-gjs-type="info-image-section"].info-image-section');
    
                    if (infoSection) {
                        this.infoSectionController.deleteInfoImageOrDesc((infoSection as HTMLElement).id);
                    }
                } else {
                    this.contentDataManager.deleteContentImage();
                }
            };
    
            const confirmationBox = new ConfirmationBox(
                message,
                title,
                handleConfirmation,
            );
            confirmationBox.render(document.body);
        }
    }    

    private updateCtaButtonImage () {
        const ctaImageEditButton = (this.e.target as Element).closest('.edit-cta-image');
        if (ctaImageEditButton) {
            const cta = this.e.target.closest('[data-gjs-type="info-cta-section"]');
            const ctaParentContainer = ctaImageEditButton.closest(".img-button-container");
            (globalThis as any).ctaContainerId = ctaParentContainer ? ctaParentContainer.id : "";

            const modal = document.createElement("div");
            modal.classList.add("tb-modal");
            modal.style.display = "flex";
            const modalContent = new ImageUpload("cta", (cta as HTMLElement).id);
            modalContent.render(modal);
    
            const uploadInput = document.createElement("input");
            uploadInput.type = "file";
            uploadInput.multiple = true;
            uploadInput.accept = "image/jpeg, image/jpg, image/png";
            uploadInput.id = "fileInput";
            uploadInput.style.display = "none";
    
            document.body.appendChild(modal);
            document.body.appendChild(uploadInput);
        }
    }

    private updateCtaButtonIcon () {
        const ctaIconEditButton = (this.e.target as Element).closest('.icon-edit-button');
        const selectedComponent = (globalThis as any).selectedComponent;
        if (ctaIconEditButton && selectedComponent && selectedComponent.getClasses().includes("img-button-container")) {
            this.e.preventDefault();
            this.e.stopPropagation();
            const editButton = ctaIconEditButton.closest(".icon-edit-button") as HTMLElement;
            const templateContainer = editButton.closest(
            "[data-gjs-type='info-cta-section']"
            ) as HTMLElement;
        
            // Get the mobileFrame for positioning context
            const mobileFrame = document.getElementById(
            `${(globalThis as any).frameId}-frame`
            ) as HTMLElement;
            const iframe = mobileFrame?.querySelector(
            "iframe"
            ) as HTMLIFrameElement;
            const iframeRect = iframe?.getBoundingClientRect();
    
            // Pass the mobileFrame to the ActionListPopUp constructor
            const list = new CtaIconsListPopup(templateContainer, mobileFrame);
    
            const triggerRect = editButton.getBoundingClientRect();
    
            list.render();
        }
    }

    private getDescription () {
        if (this.page.PageType === "Information") {
            const description = this.e.target.closest('[data-gjs-type="info-desc-section"].info-desc-section');
            console.log("description", description.querySelector('.info-desc-content'));
            if (description) {
                return description.querySelector('.info-desc-content').innerHTML;
            }
        } else {
            const description = this.e.target.closest(".content-page-block");
            if (description) {
                const descComponent = this.editor.Components.getWrapper().find("#contentDescription")[0];
                if (descComponent) {
                    return descComponent.getEl().innerHTML;
                }
            }
        }        
    }

    private createButton(id: string, className: string, text: string): HTMLButtonElement {
        const btn = document.createElement('button');
        btn.id = id;
        btn.classList.add('tb-btn', className);
        btn.innerText = text;
        return btn;
    }


    private correctULTagFromQuill(html: string): string {
        if (!html) return html;
    
        // Replace <ol> blocks containing bullet-style <li> with <ul>
        const parser = new DOMParser();
        const doc = parser.parseFromString(html, "text/html");
    
        const ols = doc.querySelectorAll("ol");
    
        ols.forEach((ol) => {
            const allBullet = Array.from(ol.children).every((li) =>
                li.getAttribute("data-list") === "bullet"
            );
    
            if (allBullet) {
                const ul = document.createElement("ul");
                Array.from(ol.children).forEach((li) => {
                    ul.appendChild(li.cloneNode(true));
                });
                ol.replaceWith(ul);
            }
        });
    
        return doc.body.innerHTML;
    }

}