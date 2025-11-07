document.addEventListener('DOMContentLoaded', () => {
    const year = new Date().getFullYear();
    const copyright = document.getElementById('copyright');
    if (copyright) {
        copyright.innerHTML = `&copy; ${year} WebLayout — Bản quyền thuộc về bạn`;
    }
});