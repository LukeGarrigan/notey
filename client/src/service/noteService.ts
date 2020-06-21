import axios from 'axios';
class NoteService {

    public static async getNotes() {
        const response = await axios.get('ttp://localhost:7071/api/notes');
        return response;
    }
}

export { NoteService as noteService };
