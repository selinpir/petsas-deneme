//-------faturayazdır2---------------------------
function yazdirFatura() {
    var faturaElement = document.getElementById("faturaAlani");
    if (!faturaElement) {
        alert("Fatura alanı bulunamadı!");
        return;
    }
    var yazdirPencere = window.open('', '', 'height=700,width=700');

    yazdirPencere.document.write('<html><head><title>Fatura Yazdır</title>');
    /*    yazdirPencere.document.write('<link rel="stylesheet" href="/fatura.css" />'); */
/*css razorda tanımlanıdğı için css üzerinden erişemedi burada tanımlandı*/
    yazdirPencere.document.write(`
        <style>
            body {
                font-family: Arial, sans-serif;
                margin: 20px;
                color: #000;
            }

            h5 b {
                font-weight: bold;
            }

            h5, p {
                margin: 5px 0;
                font-size: 16px;
            }

            .receipt-main {
                padding: 20px;
                border: 1px solid #ccc;
                border-radius: 10px;
                width: 700px;
                margin: auto;
            }

            .fa {
                margin-left: 5px;
            }
        </style>
    `);
    yazdirPencere.document.write('</head><body>');
    yazdirPencere.document.write(faturaElement.outerHTML);
    yazdirPencere.document.write('</body></html>');

    yazdirPencere.document.close();
    yazdirPencere.focus();

    yazdirPencere.print();
    yazdirPencere.close();
}