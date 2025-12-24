export function initSidebar() {
    const toggleBtn = document.getElementById("sidebarToggle");
    const sidebar = document.getElementById("sidebar");

    if (toggleBtn && sidebar) {
        toggleBtn.addEventListener("click", function (e) {
            e.preventDefault();
            sidebar.classList.toggle("collapsed");
        });
    }
}