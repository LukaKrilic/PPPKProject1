using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Model
{
    public enum ExamType
    {
        CT,
        MR,
        ULTRA,
        EKG,
        ECHO,
        OKO,
        DERM,
        DENTA,
        MAMMO,
        EEG
    }
    public class SpecialistExam
    {
        public long Id { get; set; }

        public ExamType TipPregleda { get; set; }
        public DateTime DatumPregleda { get; set; }

        public long PacijentId { get; set; }
        public Patient Pacijent { get; set; } = null!;

        public long DoctorId { get; set; }
        public Doctor Doktor { get; set; } = null!;
    }
}
