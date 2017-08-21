{// Promenljive 

    var jmbg = document.getElementById("jmbg");

    // Zelena boja - #93f484
    // Crvena boja - #f74242

}

{// JMBG Validacija 

    var dataValidator = (function proveraJMBG() {
        "use strict";

        function dataValidator() { }

        dataValidator.prototype.validanBroj = function (vrednost) {
            if (typeof vrednost !== "undefined" && vrednost !== null) {
                vrednost = vrednost.replace(',', '.');
                return !isNaN(parseFloat(vrednost)) && isFinite(vrednost);
            }
            document.getElementById("potvrda").disabled = true; return false;
        };

        dataValidator.prototype.validanDatum = function (vrednost) {
            if (Object.prototype.toString.call(vrednost) === "[object Date]")
                return !isNaN(vrednost.getTime());
            document.getElementById("potvrda").disabled = true; return false;
        };

        dataValidator.prototype.validanJMBG = function (jmbg) {
            if (typeof jmbg !== "undefined" && jmbg !== null && jmbg.length === 13 && dataValidator.prototype.validanBroj(jmbg)) {
                var dan = parseInt(jmbg.substring(0, 2), 10);
                var mesec = parseInt(jmbg.substring(2, 4), 10) - 1;
                var godina = parseInt("2" + jmbg.substring(4, 7), 10);
                if (dataValidator.prototype.validanDatum(new Date(godina, mesec, dan))) {
                    return /^60|66$/.test(jmbg.substring(7, 9)) ||
                    parseInt(jmbg.charAt(12), 10) === mod11(jmbg.substring(0, 12), function (kb) { return kb === 11 ? 0 : ((kb === 10) ? "X" : kb); });
                }
            }
            document.getElementById("potvrda").disabled = true; return false;
        };

        dataValidator.prototype.validanMod97 = function (broj, validation_error_callback) {
            if (typeof validation_error_callback !== "undefined") {
                var validation_errors = [];
                if (broj.length <= 2)
                    validation_errors.push("Nevalidna dužina broja");
                for (var i = 0; i < broj.length; i++) {
                    var validno = false;
                    var slovo = broj.charAt(i);
                    if (slovoUBroj(slovo) !== null) validno = true;
                    else validation_errors.push("Nevalidan znak: (\'" + slovo + "\') na poziciji " + (i + 1).toString());
                }
                if (validation_errors.length !== 0)
                    validation_error_callback(validation_errors);
            }
            return dataValidator.prototype.kontrolniBrojMod97(broj.substring(2)) === broj.substring(0, 2);
        };

        dataValidator.prototype.kontrolniBrojMod97 = function (broj) {
            if (typeof broj === "undefined" || broj === null || broj === "") return null;
            var zakontrolu = "";
            for (var i = 0; i < broj.length; i++) {
                var vrednost = slovoUBroj(broj.charAt(i));
                if (vrednost === null) return null;
                else zakontrolu += vrednost.toString();
            }
            if (dataValidator.prototype.validanBroj(zakontrolu)) {
                var rez = mod97(zakontrolu).toString();
                return rez.length === 1 ? rez = "0" + rez : rez;
            }
            else return null;
        };

        dataValidator.prototype.kontrolniBrojMod22 = function (broj) {
            return (dataValidator.prototype.validanBroj(broj)) ? mod22(broj).toString() : null;
        };

        dataValidator.prototype.kontrolniBrojMod11 = function (broj, dodatni_uslov) {
            return (dataValidator.prototype.validanBroj(broj)) ? mod11(broj, dodatni_uslov).toString() : null;
        };

        dataValidator.prototype.kontrolniBrojMod11Sub = function (broj, dodatni_uslov) {
            return (dataValidator.prototype.validanBroj(broj)) ? mod11sub(broj, dodatni_uslov).toString() : null;
        };

        function slovoUBroj(slovo) {
            return slovo === "-" ? "" :
				((dataValidator.prototype.validanBroj(slovo)) ? slovo : (_slova_za_kontrolni_broj[slovo] || null));
        }

        function mod97(br) {
            var kb = 0, os = 100;
            for (var x = br.length - 1; x >= 0; x--) {
                kb = (kb + (os * parseInt(br.charAt(x), 10))) % 97;
                os = (os * 10) % 97;
            }
            kb = 98 - kb;
            return kb;
        }

        function mod22(br) {
            var kb = mod11sub(br);
            return kb > 9 ? kb -= 10 : kb;
        }

        function mod11(br, dodatni_uslov) {
            var kb = 0;
            for (var i = br.length - 1, mnozilac = 2; i >= 0; i--) {
                kb += parseInt(br.charAt(i), 10) * mnozilac;
                mnozilac = mnozilac === 7 ? 2 : mnozilac + 1;
            }
            kb = 11 - (kb % 11);
            return (typeof dodatni_uslov === "undefined") ? kb : dodatni_uslov(kb);
        }

        function mod11sub(br, dodatni_uslov) {
            var kb = 0;
            for (var i = 0, mnozilac = 7; i < br.length; i++) {
                kb += parseInt(br.charAt(i), 10) * mnozilac;
                mnozilac = mnozilac === 2 ? 7 : mnozilac - 1;
            }
            kb = 11 - (kb % 11);
            return (typeof dodatni_uslov === "undefined") ? kb : dodatni_uslov(kb);
        }

        var _slova_za_kontrolni_broj =
        {
            "A": 10, "B": 11, "C": 12, "D": 13, "E": 14, "F": 15, "G": 16, "H": 17, "I": 18, "J": 19,
            "K": 20, "L": 21, "M": 22, "N": 23, "O": 24, "P": 25, "Q": 26, "R": 27, "S": 28, "T": 29,
            "U": 30, "V": 31, "W": 32, "X": 33, "Y": 34, "Z": 35
        };
        return dataValidator;

    })();

    var JMBG = function (jmbg) {
        "use strict";
        var _jmbg, _region, _dan, _mesec, _godina, _rbr, _kontrolni;
        var _validan = dataValidator.prototype.validanJMBG(jmbg);
        if (_validan) {
            _jmbg = jmbg;
            Parse();
        }

        this.pol = function () {
            if (_validan) return _rbr > 500 ? "Z" : "M";
            return null;
        };

        this.region = function () {
            return regioni[_region] || null;
        };

        this.redniBrojRodjenja = function () {
            if (_validan) return _rbr > 499 ? _rbr - 499 : _rbr + 1;
            else return null;
        };

        this.validan = function () {
            return _validan;
        };

        this.datumRodjenja = function () {
            return _validan ? new Date(_godina, _mesec, _dan) : null;
        };

        this.toString = function () {
            return _validan ? _jmbg : null;
        };

        function Parse() {
            _dan = parseInt(_jmbg.substring(0, 2), 10);
            _mesec = parseInt(_jmbg.substring(2, 4), 10) - 1;
            _godina = parseInt(_jmbg.substring(4, 7), 10);
            _region = _jmbg.substring(7, 9);
            _rbr = parseInt(_jmbg.substring(9, 12), 10);
            _kontrolni = parseInt(_jmbg.charAt(12), 10);

            var tekuca_god = new Date().getFullYear() % 1000;
            var tekuci_mil = new Date().getFullYear() - tekuca_god;

            if (_godina > tekuca_god)
                _godina += tekuci_mil - 1000;
            else _godina += tekuci_mil;
        }
    };
}

