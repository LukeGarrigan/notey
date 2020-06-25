import axios from 'axios';
import {NoteViewModel} from '@/models/NoteViewModel';
class NoteService {

    public static async getNotes() {

        try {
            const response = await axios.get('http://localhost:7071/api/notes');

            const notes = [];
            for (const data of response.data) {
                const note: NoteViewModel = {
                    id: data.rowKey,
                    markdown: data.note,
                    title: data.rowKey
                };
                notes.push(note);
            }
            return notes;
        } catch {
            return '';
        }

    }
}

export { NoteService as noteService };
