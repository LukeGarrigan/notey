import axios from 'axios';
import {NoteViewModel} from '@/models/NoteViewModel';
class NoteService {

    public static async getNotes() {

        try {
            const response = await axios.get('http://localhost:7071/api/notes');
            const note: NoteViewModel = {
                id: response.data.rowKey,
                markdown: response.data.note,
                title: 'test note'
            };
            const notes = [];

            notes.push(note);
            return notes;
        } catch {
            return '';
        }

    }
}

export { NoteService as noteService };
