import Vue from 'vue'
import Router from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Logout from '../views/Logout.vue'
import Register from '../views/Register.vue'
import store from '../store/index'
//import DrinkList from '../components/DrinkList.vue'
import Restaurant from '../views/Restaurant.vue'
import Socials from '../views/Socials.vue'
import Invite from '../components/InviteUserForm.vue'
import updateDrinkForm from'../views/UpdateDrinkForm.vue'
import ReviewList from '../components/ReviewList.vue'
import SearchDrinksInput from '../components/SearchDrinksInput.vue'
Vue.use(Router)

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */

const router = new Router({
    mode: 'history',
    base: process.env.BASE_URL,
    routes: [{
            path: '/',
            name: 'home',
            component: Home,
            meta: {
                requiresAuth: false
            }
        },
        {
            path: "/login",
            name: "login",
            component: Login,
            meta: {
                requiresAuth: false
            }
        },
        {
            path: "/logout",
            name: "logout",
            component: Logout,
            meta: {
                requiresAuth: false
            }
        },
        {
            path: "/register",
            name: "register",
            component: Register,
            meta: {
                requiresAuth: false
            }
        },
        // {
        //     path: "/restaurant/:id/drinks",
        //     name: "drink-list",
        //     component: Restaurant
        // },
        {
            path: "/restaurant/:id",
            name: "restaurant-view",
            component: Restaurant
        },
        {
            path: "/socials",
            name: "socials",
            component: Socials,
            meta: {
                requiresAuth: false
            }
        },
        {
            path: "/invite",
            name: "invite",
            component: Invite
        },
        {
        path: "/restaurant/:id/drinks/:drinkId",
        name: "update-drink",
        component: updateDrinkForm
        },
        {
            path: "/restaurant/:id/reviews",
            name: "restaurant-reviews",
            component: ReviewList
        },
        {
            path: "/searchDrinks",
            name: "search-drinks",
            component: SearchDrinksInput
        }
    ]
})

router.beforeEach((to, from, next) => {
    // Determine if the route requires Authentication
    const requiresAuth = to.matched.some(x => x.meta.requiresAuth);

    // If it does and they are not logged in, send the user to "/login"
    if (requiresAuth && store.state.token === '') {
        next("/login");
    } else {
        // Else let them go to their next destination
        next();
    }
});

export default router;