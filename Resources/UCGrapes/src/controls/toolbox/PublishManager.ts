import { i18n } from "../../i18n/i18n";
import { ToolBoxService } from "../../services/ToolBoxService";
import { Alert } from "../../ui/components/Alert";
import { Modal } from "../../ui/components/Modal";
import { AppVersionManager } from "../versions/AppVersionManager";
import { ToolboxManager } from "./ToolboxManager";

export class PublishManager {
  toolboxService: any;
  toolBoxManager: ToolboxManager;
  appVersions: AppVersionManager;
  constructor() {
    this.toolboxService = new ToolBoxService();
    this.toolBoxManager = new ToolboxManager()
    this.appVersions = new AppVersionManager();
  }

  openModal() {
    const div = document.createElement("div");
    const p = document.createElement("p");
    p.innerText = i18n.t("navbar.publish.modal_description");
    
    const label = document.createElement("label") as HTMLLabelElement;
    label.className = "notify_residents";

    const input = document.createElement("input") as HTMLInputElement;
    input.type = "checkbox";
    input.id = "notify_residents";
    input.name = "notify_residents";

    const span = document.createElement("span") as HTMLSpanElement;
    span.innerText = i18n.t("navbar.publish.notify_residents");

    label.appendChild(input);
    label.appendChild(span);

    const submitSection = document.createElement("div");
    submitSection.classList.add("popup-footer");
    submitSection.style.marginBottom = "-12px";

    const saveBtn = this.createButton(
      "submit_publish",
      "tb-btn-primary",
      i18n.t("navbar.publish.modal_confirm")
    );
    
    const cancelBtn = this.createButton(
      "cancel_publish",
      "tb-btn-outline",
      i18n.t("navbar.publish.modal_cancel")
    );

    submitSection.appendChild(saveBtn);
    submitSection.appendChild(cancelBtn);

    div.appendChild(p);
    div.appendChild(label);
    div.appendChild(submitSection);

    const modal = new Modal({
      title: i18n.t("navbar.publish.modal_title"),
      width: "500px",
      body: div,
    });

    modal.open();

    saveBtn.addEventListener("click", async (e) => {
      e.preventDefault();
      modal.close();
      this.toolBoxManager.savePages(false).then(res => {
        this.appVersions.getActiveVersion().then(res=>{
          this.toolboxService.publishAppVersion(res.AppVersionId, input.checked)
          .then((res:any)=>{
            if(!res.message){
              modal.close()
              new Alert("success", i18n.t("messages.success.published"))
            }
          })
        })
      })
    });

    cancelBtn.addEventListener("click", (e) => {
      e.preventDefault();
      modal.close();
    });
  }
  public processData() {
    // process the data from here
  }

  
  private createButton(id: string, className: string, text: string): HTMLButtonElement {
    const btn = document.createElement('button');
    btn.id = id;
    btn.classList.add('tb-btn', className);
    btn.innerText = text;
    return btn;
 }
}
