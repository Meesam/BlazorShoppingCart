/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./Components/**/*.{html,js,razor,cshtml}", "./node_modules/flowbite/**/*.js"],
    theme: {
        extend: {},
    },
    plugins: [require('flowbite/plugin')],
}

