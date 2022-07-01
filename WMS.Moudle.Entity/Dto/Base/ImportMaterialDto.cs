
using Npoi.Mapper.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WMS.Moudle.Entity.Dto.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class ImportMaterialDto
    {
        private string _fabrication_no;
        /// <summary>
        /// 部件编号(唯一)
        /// </summary>
        [Column("NO. FABRICATION")]
        public string fabrication_no
        {
            get
            {
                return _fabrication_no;
            }
            set
            {
                _fabrication_no = value.Trim();
            }
        }

        private string _piece_code;
        /// <summary>
        /// 部件组编号
        /// </summary>
        [Column("CODE_PIECE")]
        public string piece_code
        {
            get
            {
                return _piece_code;
            }
            set
            {
                _piece_code = value.Trim();
            }
        }

        private string _etat_code;
        /// <summary>
        /// 状态码
        /// </summary>
        [Column("Code ETAT")]
        public string etat_code
        {
            get { return _etat_code; }
            set { _etat_code = value.Trim(); }
        }

        /// <summary>
        /// 有效期时间
        /// </summary>
        [Column("Date du code ETAT")]
        public DateTime? due_date { get; set; }

        private string _usine_code;
        [Column("CODE USINE")]
        public string usine_code
        {
            get { return _usine_code; }
            set { _usine_code = value.Trim(); }
        }

        private string _localisation_code;
        [Column("CODE LOCALISATION")]
        public string localisation_code
        {
            get { return _localisation_code; }
            set { _localisation_code = value.Trim(); }
        }

        private string _localisation;
        [Column("LOCALISATION")]
        public string localisation
        {
            get { return _localisation; }
            set { _localisation = value.Trim(); }
        }

        private string _associe_no;
        [Column("NO. ASSOCIE")]
        public string associe_no
        {
            get { return _associe_no; }
            set { _associe_no = value.Trim(); }
        }

        private string _renovations_nbr;
        [Column("Nbr RENOVATIONS")]
        public string renovations_nbr
        {
            get { return _renovations_nbr; }
            set { _renovations_nbr = value.Trim(); }
        }

        private string _renovable_code;
        [Column("Code RENOVABLE")]
        public string renovable_code
        {
            get { return _renovable_code; }
            set { _renovable_code = value.Trim(); }
        }

        private string _nbr_cuissons_totales;
        [Column("Nbr CUISSONS Totales")]
        public string nbr_cuissons_totales
        {
            get { return _nbr_cuissons_totales; }
            set { _nbr_cuissons_totales = value.Trim(); }
        }

        private string _gestionnaire_code;
        [Column("Code gestionnaire")]
        public string gestionnaire_code
        {
            get { return _gestionnaire_code; }
            set { _gestionnaire_code = value.Trim(); }
        }

        private string _gep_code;
        [Column("CODE GEP")]
        public string gep_code
        {
            get { return _gep_code; }
            set { _gep_code = value.Trim(); }
        }

        private string _eventation_no;
        [Column("NO EVENTATION")]
        public string eventation_no
        {
            get { return _eventation_no; }
            set { _eventation_no = value.Trim(); }
        }

        /// <summary>
        /// 
        /// </summary>
        [Column("Date DISPONIBILITE")]
        public DateTime? disponibilite_date { get; set; }

        private string _dispo_equ;
        [Column("EQU DISPO")]
        public string dispo_equ
        {
            get { return _dispo_equ; }
            set { _dispo_equ = value.Trim(); }
        }

        private string _assemble_moule_no;
        [Column("NO MOULE ASSEMBLE")]
        public string assemble_moule_no
        {
            get { return _assemble_moule_no; }
            set { _assemble_moule_no = value.Trim(); }
        }

        private string _consigne_codegep_date;
        [Column("Date consigne CODEGEP")]
        public string consigne_codegep_date
        {
            get { return _consigne_codegep_date; }
            set { _consigne_codegep_date = value.Trim(); }
        }

        /// <summary>
        /// Date der. MOUVEMENT
        /// </summary>
        [Column("Date der. MOUVEMENT")]
        public DateTime? der_mouvement_date { get; set; }

        private string _piece_type;
        /// <summary>
        /// 
        /// </summary>
        [Column("Type PIECE")]
        public string piece_type
        {
            get { return _piece_type; }
            set { _piece_type = value.Trim(); }
        }

        private string _facultative_evt;
        /// <summary>
        /// EVT Facultative
        /// </summary>
        [Column("EVT Facultative")]
        public string facultative_evt
        {
            get { return _facultative_evt; }
            set { _facultative_evt = value.Trim(); }
        }

        private string _gar_ou_sup;
        [Column("Gar ou Sup")]
        public string gar_ou_sup
        {
            get { return _gar_ou_sup; }
            set { _gar_ou_sup = value.Trim(); }
        }

        private string _nomv_der_utilisation;
        [Column("NOMV der Utilisation")]
        public string nomv_der_utilisation
        {
            get { return _nomv_der_utilisation; }
            set { _nomv_der_utilisation = value.Trim(); }
        }

        private string _affectation;
        [Column("Affectation")]
        public string affectation
        {
            get { return _affectation; }
            set { _affectation = value.Trim(); }
        }

        private string _der_rpq;
        [Column("Der RPQ")]
        public string der_rpq
        {
            get { return _der_rpq; }
            set { _der_rpq = value.Trim(); }
        }

        private string _der_cb;
        [Column("Der CB")]
        public string der_cb
        {
            get { return _der_cb; }
            set { _der_cb = value.Trim(); }
        }

        private string _der_cai_lpc;
        [Column("Der. CAI/LPC")]
        public string der_cai_lpc
        {
            get { return _der_cai_lpc; }
            set { _der_cai_lpc = value.Trim(); }
        }

        private string _dimension;
        [Column("Dimension")]
        public string dimension
        {
            get { return _dimension; }
            set { _dimension = value.Trim(); }
        }

        private string _sculpture;
        [Column("Sculpture")]
        public string sculpture
        {
            get { return _sculpture; }
            set { _sculpture = value.Trim(); }
        }

        private string _profil;
        [Column("Profil")]
        public string profil
        {
            get { return _profil; }
            set { _profil = value.Trim(); }
        }

        private string _type_de_moule;
        [Column("Type de Moule")]
        public string type_de_moule
        {
            get { return _type_de_moule; }
            set { _type_de_moule = value.Trim(); }
        }
    }
}
