import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { Photo } from '../../_models/photo';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() photos: Photo[];

 uploader: FileUploader = new FileUploader({url: URL});
 hasBaseDropZoneOver = false;
 hasAnotherDropZoneOver = false;


  constructor() { }

  ngOnInit() {
  }

 fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

}
