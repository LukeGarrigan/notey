import axios from 'axios';
import {NoteViewModel} from '@/models/NoteViewModel';
import {AddNoteRequest} from '@/models/requests/AddNoteRequest';
class NoteService {

    public static async getNotes() {

        try {
            const response = await axios.get('http://localhost:7071/api/notes');

            const notes = [];
            for (const data of response.data) {
                const note: NoteViewModel = {
                    id: data.rowKey,
                    markdown: data.markdown,
                    title: data.title,
                    lastUpdateTime: data.lastUpdateTime
                };
                notes.push(note);
            }
            return notes;
        } catch {
            return '';
        }

    }

    public static async saveNote(note: AddNoteRequest) {
        const response = await axios.post('http://localhost:7071/api/AddOrUpdateNote', note);
        return response.data;
    }

    public static async deleteNote(noteId: string) {
        await axios.delete(`http://localhost:7071/api/notes/${noteId}`);
    }

    public static async searchNote(value: any) {
        const response = await axios.get(`http://localhost:7071/api/notes/${value}/search`);

        const notes = [];
        for (const data of response.data) {
            const note: NoteViewModel = {
                id: data.rowKey,
                markdown: data.markdown,
                title: data.title,
                lastUpdateTime: data.lastUpdateTime
            };
            notes.push(note);
        }
        return notes;

    }
}

export { NoteService as noteService };
