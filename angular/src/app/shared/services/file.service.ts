import { Injectable } from '@angular/core';
import { getDownloadURL, getStorage, ref } from '@angular/fire/storage';

@Injectable({
  providedIn: 'root',
})
export class FileService {
  public async GetFileFromFirebase(fileName: string): Promise<string> {
    const storage = getStorage();

    const url = await getDownloadURL(ref(storage, 'images/' + fileName));
    console.log(url);
    return url;
  }
}
