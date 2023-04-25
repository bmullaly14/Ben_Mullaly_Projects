import axios from 'axios';

const http = axios.create({
    baseURL: "https://localhost:44315/"
});

export default {
    getAllDrinks(){
        return http.get('drink');
    },

    getDrinkById(id){
        return http.get(`drink/${id}`);
    },

    addNewDrink(drink){
        return http.post('drink', drink);
    },

    updateDrink(id,drink){
        return http.put(`drink/${id}`, drink);
    },
    
    deleteDrink(id){
        return http.delete(`drink/${id}`);
    }
}