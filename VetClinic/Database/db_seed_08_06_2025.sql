-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Cze 08, 2025 at 03:40 PM
-- Wersja serwera: 10.4.32-MariaDB
-- Wersja PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `vetclinic`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `admin`
--

CREATE TABLE `admin` (
  `id` int(11) DEFAULT NULL,
  `name` varchar(255) NOT NULL,
  `surname` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Dumping data for table `admin`
--

INSERT INTO `admin` (`id`, `name`, `surname`) VALUES
(1, 'System', 'Admin');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `appointment`
--

CREATE TABLE `appointment` (
  `id` int(11) NOT NULL,
  `pet_id` int(11) NOT NULL,
  `doctor_id` int(11) NOT NULL,
  `reason_for_visit` varchar(255) NOT NULL,
  `notes` varchar(255) DEFAULT NULL,
  `diagnosis` varchar(255) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `appointment_date` datetime NOT NULL,
  `status_id` int(11) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Dumping data for table `appointment`
--

INSERT INTO `appointment` (`id`, `pet_id`, `doctor_id`, `reason_for_visit`, `notes`, `diagnosis`, `created_at`, `appointment_date`, `status_id`) VALUES
(1, 1, 4, 'Annual check-up and vaccinations', 'Dog is active and healthy.', 'Healthy, up-to-date on vaccines', '2025-06-03 10:00:00', '2025-06-06 10:00:00', 1), -- Shifted to current week, scheduled
(2, 2, 5, 'Lethargy and loss of appetite', 'Cat has not eaten for 2 days. Vomiting once.', 'Possible gastrointestinal issue', '2025-06-03 14:30:00', '2025-06-06 14:00:00', 1), -- Shifted to current week, scheduled
(3, 3, 4, 'Limpping on hind leg', 'Golden Retriever felt pain after playing.', 'Suspected sprain, needs X-ray', '2025-06-04 09:00:00', '2025-06-07 11:00:00', 1), -- Shifted to current week, scheduled
(4, 1, 4, 'Vaccination booster', 'Routine shot, pet active.', 'Vaccinated', '2025-05-27 09:00:00', '2025-05-29 09:00:00', 3), -- Shifted to previous week, completed
(5, 2, 4, 'Flea treatment', 'Heavy flea infestation.', 'Fleas treated', '2025-05-27 10:00:00', '2025-05-29 10:00:00', 3), -- Shifted to previous week, completed
(6, 3, 4, 'Ear infection check', 'Itching ears.', 'Otitis externa', '2025-05-28 11:00:00', '2025-05-29 11:00:00', 3), -- Shifted to previous week, completed
(7, 4, 4, 'Routine check-up', 'Cat seems healthy.', 'Healthy', '2025-05-27 13:00:00', '2025-05-30 09:30:00', 3), -- Shifted to previous week, completed
(8, 5, 4, 'Nail trim', 'Nails overgrown.', 'Nails trimmed', '2025-05-28 14:00:00', '2025-05-30 10:30:00', 3), -- Shifted to previous week, completed
(9, 6, 4, 'Dental check', 'Bad breath.', 'Gingivitis, needs cleaning', '2025-05-29 09:00:00', '2025-05-30 11:30:00', 3), -- Shifted to previous week, completed
(10, 1, 4, 'Follow-up ear infection', 'Improvement noted.', 'Healing well', '2025-05-29 15:00:00', '2025-05-31 09:00:00', 3), -- Shifted to previous week, completed
(11, 2, 4, 'Weight management consultation', 'Owner concerned about weight.', 'Diet plan initiated', '2025-05-30 10:00:00', '2025-05-31 10:00:00', 3), -- Shifted to previous week, completed
(12, 3, 4, 'Annual vaccinations', 'Routine.', 'Vaccinated', '2025-05-30 11:00:00', '2025-05-31 11:00:00', 3), -- Shifted to previous week, completed
(13, 4, 4, 'Lethargy investigation', 'Cat sleeping more than usual.', 'Mild viral infection', '2025-05-31 09:00:00', '2025-06-02 09:00:00', 3), -- Shifted to previous week, completed
(14, 5, 4, 'Deworming', 'Routine deworming.', 'Dewormed', '2025-05-31 10:00:00', '2025-06-02 10:00:00', 3), -- Shifted to previous week, completed
(15, 6, 4, 'Diarrhea', 'Rabbit has soft stools.', 'Dietary indiscretion', '2025-06-01 11:00:00', '2025-06-02 11:00:00', 3), -- Shifted to previous week, completed
(16, 1, 4, 'Skin rash', 'Redness on abdomen.', 'Allergic reaction', '2025-06-01 14:00:00', '2025-06-03 09:00:00', 1), -- Shifted to current week, scheduled
(17, 2, 4, 'Eye discharge', 'Sticky eyes.', 'Conjunctivitis', '2025-06-02 10:00:00', '2025-06-03 10:00:00', 1), -- Shifted to current week, scheduled
(18, 3, 4, 'Check on limping (X-ray follow-up)', 'Owner reports improvement.', 'Recovering well', '2025-06-02 11:00:00', '2025-06-03 11:00:00', 1), -- Shifted to current week, scheduled
(19, 4, 4, 'Appetite loss', 'Not eating well for 2 days.', 'Stress-related', '2025-06-03 09:00:00', '2025-06-04 09:00:00', 1), -- Shifted to current week, scheduled
(20, 5, 4, 'Regular check-up', 'Annual visit for Labrador.', 'Healthy', '2025-06-03 10:00:00', '2025-06-04 10:00:00', 1), -- Shifted to current week, scheduled
(21, 6, 4, 'Routine check-up', 'Rabbit health check.', 'Healthy', '2025-06-03 14:00:00', '2025-06-04 14:00:00', 1), -- Shifted to current week, scheduled
(22, 1, 4, 'Allergy medication refill', 'Ongoing medication.', 'Stable', '2025-06-04 09:00:00', '2025-06-04 15:00:00', 1), -- Shifted to current week, scheduled
(23, 2, 4, 'Recheck eye infection', 'Eyes clearing up.', 'Almost fully recovered', '2025-06-04 10:00:00', '2025-06-04 16:00:00', 1), -- Shifted to current week, scheduled
(24, 1, 5, 'Digestive upset', 'Vomiting and diarrhea.', 'Gastroenteritis', '2025-05-27 10:00:00', '2025-05-29 09:30:00', 4), -- Shifted to previous week, cancelled
(25, 2, 5, 'Urinary issues', 'Frequent urination, straining.', 'UTI suspected', '2025-05-27 11:00:00', '2025-05-29 10:30:00', 4), -- Shifted to previous week, cancelled
(26, 3, 5, 'Chronic cough', 'Coughing for a few weeks.', 'Possible kennel cough', '2025-05-28 12:00:00', '2025-05-29 11:30:00', 4), -- Shifted to previous week, cancelled
(27, 4, 5, 'Sudden lameness', 'Limping on front paw.', 'Minor sprain', '2025-05-27 14:00:00', '2025-05-30 10:00:00', 4), -- Shifted to previous week, cancelled
(28, 5, 5, 'Skin irritation', 'Red patches on skin.', 'Allergic dermatitis', '2025-05-28 15:00:00', '2025-05-30 11:00:00', 4), -- Shifted to previous week, cancelled
(29, 6, 5, 'Loss of balance', 'Stumbling and disoriented.', 'Inner ear infection', '2025-05-29 10:00:00', '2025-05-30 12:00:00', 4), -- Shifted to previous week, cancelled
(30, 1, 5, 'Recheck gastroenteritis', 'Symptoms subsided.', 'Fully recovered', '2025-05-29 16:00:00', '2025-05-31 09:30:00', 4), -- Shifted to previous week, cancelled
(31, 2, 5, 'UTI follow-up', 'Urine sample taken.', 'Confirmed UTI, starting antibiotics', '2025-05-30 12:00:00', '2025-05-31 10:30:00', 4), -- Shifted to previous week, cancelled
(32, 3, 5, 'Kennel cough follow-up', 'Cough less severe.', 'Improving', '2025-05-30 13:00:00', '2025-05-31 11:30:00', 4), -- Shifted to previous week, cancelled
(33, 4, 5, 'Annual check-up', 'Routine visit for cat.', 'Healthy', '2025-05-31 10:00:00', '2025-06-02 09:30:00', 4), -- Shifted to previous week, cancelled
(34, 5, 5, 'Behavioral consultation', 'Excessive barking.', 'Anxiety related', '2025-05-31 11:00:00', '2025-06-02 10:30:00', 4), -- Shifted to previous week, cancelled
(35, 6, 5, 'Eye injury', 'Scratch on eye.', 'Corneal abrasion', '2025-06-01 12:00:00', '2025-06-02 11:30:00', 4), -- Shifted to previous week, cancelled
(36, 1, 5, 'Routine vaccination', 'Booster shot.', 'Vaccinated', '2025-06-01 15:00:00', '2025-06-03 09:30:00', 1), -- Shifted to current week, scheduled
(37, 2, 5, 'Dental check-up', 'Examining teeth.', 'Needs dental cleaning', '2025-06-02 11:00:00', '2025-06-03 10:30:00', 1), -- Shifted to current week, scheduled
(38, 3, 5, 'Grooming advise', 'Owner asking for advice on fur care.', 'Advised regular brushing', '2025-06-02 12:00:00', '2025-06-03 11:30:00', 1), -- Shifted to current week, scheduled
(39, 4, 5, 'Fever', 'Lethargic with high temperature.', 'Viral infection', '2025-06-03 10:00:00', '2025-06-04 09:30:00', 1), -- Shifted to current week, scheduled
(40, 5, 5, 'Arthritis management', 'Senior dog with joint pain.', 'Prescribed joint supplements', '2025-06-03 11:00:00', '2025-06-04 10:30:00', 1), -- Shifted to current week, scheduled
(41, 6, 5, 'Follow-up eye injury', 'Eye looks better.', 'Healing well', '2025-06-03 15:00:00', '2025-06-04 14:30:00', 1), -- Shifted to current week, scheduled
(42, 1, 5, 'Annual check-up', 'Routine check.', 'Healthy', '2025-06-04 10:00:00', '2025-06-04 15:30:00', 1), -- Shifted to current week, scheduled
(43, 2, 5, 'Weight loss program', 'Reviewing diet for overweight cat.', 'Progressing well', '2025-06-04 11:00:00', '2025-06-04 16:30:00', 1), -- Shifted to current week, scheduled
(44, 1, 8, 'Allergy flare-up', 'Severe itching and redness.', 'Atopic dermatitis', '2025-05-27 11:00:00', '2025-05-29 10:00:00', 3), -- Shifted to previous week, completed
(45, 2, 8, 'Hot spots', 'Licking and chewing on flank.', 'Bacterial dermatitis', '2025-05-27 12:00:00', '2025-05-29 11:00:00', 3), -- Shifted to previous week, completed
(46, 3, 8, 'Skin growth', 'New lump on side.', 'Benign lipoma', '2025-05-28 13:00:00', '2025-05-29 12:00:00', 3), -- Shifted to previous week, completed
(47, 4, 8, 'Excessive shedding', 'Owner concerned about hair loss.', 'Seasonal shedding', '2025-05-27 15:00:00', '2025-05-30 10:30:00', 3), -- Shifted to previous week, completed
(48, 5, 8, 'Pustules on skin', 'Small bumps with pus.', 'Pyoderma', '2025-05-28 16:00:00', '2025-05-30 11:30:00', 3), -- Shifted to previous week, completed
(49, 6, 8, 'Mite infestation', 'Itching and hair loss around ears.', 'Ear mites', '2025-05-29 11:00:00', '2025-05-30 12:30:00', 3), -- Shifted to previous week, completed
(50, 1, 8, 'Atopic dermatitis follow-up', 'Skin calming down.', 'Improvement, continue medication', '2025-05-29 17:00:00', '2025-05-31 10:00:00', 3), -- Shifted to previous week, completed
(51, 2, 8, 'Hot spots recheck', 'Lesions healing.', 'Healing well', '2025-05-30 13:00:00', '2025-05-31 11:00:00', 3), -- Shifted to previous week, completed
(52, 3, 8, 'Annual skin check', 'Routine examination.', 'No new concerns', '2025-05-30 14:00:00', '2025-05-31 12:00:00', 3), -- Shifted to previous week, completed
(53, 4, 8, 'Allergy testing', 'Preparing for full allergy panel.', 'Scheduled for test', '2025-05-31 11:00:00', '2025-06-02 10:00:00', 3), -- Shifted to previous week, completed
(54, 5, 8, 'Skin infection follow-up', 'Skin less inflamed.', 'Progressing positively', '2025-05-31 12:00:00', '2025-06-02 11:00:00', 3), -- Shifted to previous week, completed
(55, 6, 8, 'Hair loss patches', 'Circular hair loss on back.', 'Fungal infection suspected', '2025-06-01 13:00:00', '2025-06-02 12:00:00', 3), -- Shifted to previous week, completed
(56, 1, 8, 'Food allergy consultation', 'Discussing dietary changes.', 'Recommended hypoallergenic diet', '2025-06-01 16:00:00', '2025-06-03 10:00:00', 1), -- Shifted to current week, scheduled
(57, 2, 8, 'Chronic itching', 'Ongoing scratching despite previous treatment.', 'Further diagnostics needed', '2025-06-02 12:00:00', '2025-06-03 11:00:00', 1), -- Shifted to current week, scheduled
(58, 3, 8, 'Dull coat', 'Lack of shine in fur.', 'Nutritional deficiency', '2025-06-02 13:00:00', '2025-06-03 12:00:00', 1), -- Shifted to current week, scheduled
(59, 4, 8, 'Itchy paws', 'Licking paws excessively.', 'Environmental allergies', '2025-06-03 11:00:00', '2025-06-04 10:00:00', 1), -- Shifted to current week, scheduled
(60, 5, 8, 'Skin biopsy results', 'Discussing findings.', 'Negative for malignancy', '2025-06-03 12:00:00', '2025-06-04 11:00:00', 1), -- Shifted to current week, scheduled
(61, 6, 8, 'Fungal infection recheck', 'Started medication.', 'Beginning to clear', '2025-06-03 16:00:00', '2025-06-04 15:00:00', 1), -- Shifted to current week, scheduled
(62, 1, 8, 'Eczema management', 'Long-term care plan.', 'Ongoing management', '2025-06-04 11:00:00', '2025-06-04 16:00:00', 1), -- Shifted to current week, scheduled
(63, 2, 8, 'Follow-up chronic itching', 'Trying new medication.', 'Monitoring response', '2025-06-04 12:00:00', '2025-06-04 17:00:00', 1), -- Shifted to current week, scheduled
(64, 3, 4, 'Dental Cleaning Prep', 'Pre-anesthetic exam.', 'Ready for dental procedure', '2025-06-05 10:00:00', '2025-06-05 09:00:00', 1), -- Shifted to current week, scheduled
(65, 4, 4, 'Post-op check', 'Following minor surgery.', 'Healing well', '2025-06-05 11:00:00', '2025-06-05 10:00:00', 1), -- Shifted to current week, scheduled
(66, 5, 4, 'Annual vaccinations', 'Routine.', 'Vaccinated', '2025-06-05 12:00:00', '2025-06-05 11:00:00', 1), -- Shifted to current week, scheduled
(67, 6, 4, 'Lump removal consult', 'Discussing surgical options.', 'Scheduled for surgery', '2025-06-05 13:00:00', '2025-06-06 09:30:00', 1), -- Shifted to current week, scheduled
(68, 1, 4, 'Coughing', 'Persistent dry cough.', 'Tracheitis', '2025-06-06 09:00:00', '2025-06-06 10:30:00', 1), -- Shifted to current week, scheduled
(69, 2, 4, 'Wellness exam', 'Healthy pet check-up.', 'Healthy', '2025-06-06 10:00:00', '2025-06-06 11:30:00', 1), -- Shifted to current week, scheduled
(70, 3, 4, 'Dental Cleaning', 'Performing procedure.', 'Successful cleaning', '2025-06-06 11:00:00', '2025-06-07 09:00:00', 1), -- Shifted to current week, scheduled
(71, 4, 4, 'Ear cleaning', 'Routine ear maintenance.', 'Ears clean', '2025-06-07 09:00:00', '2025-06-07 10:00:00', 1), -- Shifted to current week, scheduled
(72, 5, 4, 'Puppy check-up', 'First vet visit for new puppy.', 'Healthy puppy', '2025-06-07 10:00:00', '2025-06-07 11:00:00', 1), -- Shifted to current week, scheduled
(73, 6, 4, 'Follow-up diarrhea', 'Stools firming up.', 'Recovering well', '2025-06-07 11:00:00', '2025-06-09 09:00:00', 1), -- Shifted to future, scheduled
(74, 1, 4, 'Gastrointestinal upset', 'Vomiting, no diarrhea.', 'Dietary indiscretion', '2025-06-08 09:00:00', '2025-06-09 10:00:00', 1), -- Shifted to future, scheduled
(75, 2, 4, 'Skin lump check', 'New lump on leg.', 'Benign', '2025-06-08 10:00:00', '2025-06-09 11:00:00', 1), -- Shifted to future, scheduled
(76, 3, 4, 'Post-dental check', 'Checking recovery.', 'Recovering well', '2025-06-08 11:00:00', '2025-06-10 09:00:00', 1), -- Shifted to future, scheduled
(77, 4, 4, 'Vaccination', 'Annual booster.', 'Vaccinated', '2025-06-09 09:00:00', '2025-06-10 10:00:00', 1), -- Shifted to future, scheduled
(78, 5, 4, 'Limpping - acute', 'Suddenly limping on front leg.', 'Sprain', '2025-06-09 10:00:00', '2025-06-10 11:00:00', 1), -- Shifted to future, scheduled
(79, 6, 4, 'Respiratory symptoms', 'Sneezing and nasal discharge.', 'Upper respiratory infection', '2025-06-09 11:00:00', '2025-06-10 12:00:00', 1), -- Shifted to future, scheduled
(80, 1, 4, 'Follow-up cough', 'Cough improving.', 'Almost resolved', '2025-06-10 09:00:00', '2025-06-11 09:00:00', 1), -- Shifted to future, scheduled
(81, 2, 4, 'Microchipping', 'Routine microchip implantation.', 'Microchipped', '2025-06-10 10:00:00', '2025-06-11 10:00:00', 1), -- Shifted to future, scheduled
(82, 3, 4, 'New pet check-up', 'First visit for adopted dog.', 'Healthy', '2025-06-10 11:00:00', '2025-06-11 11:00:00', 1), -- Shifted to future, scheduled
(83, 4, 4, 'Recheck lameness', 'Cat still slightly limping.', 'Needs further rest', '2025-06-10 12:00:00', '2025-06-11 12:00:00', 1), -- Shifted to future, scheduled
(84, 5, 4, 'Senior check-up', 'Comprehensive exam for older dog.', 'Normal for age', '2025-06-11 09:00:00', '2025-06-11 13:00:00', 1), -- Shifted to future, scheduled
(85, 6, 4, 'Weight check', 'Monitoring rabbit weight.', 'Stable', '2025-06-11 10:00:00', '2025-06-11 14:00:00', 1), -- Shifted to future, scheduled
(86, 1, 5, 'Blood test results', 'Discussing previous test results.', 'Normal results', '2025-06-04 11:00:00', '2025-06-05 09:30:00', 1), -- Shifted to current week, scheduled
(87, 2, 5, 'UTI recheck', 'Checking on progress of treatment.', 'Improving', '2025-06-05 12:00:00', '2025-06-05 10:30:00', 1), -- Shifted to current week, scheduled
(88, 3, 5, 'Lethargy in older dog', 'Owner concerned about energy levels.', 'Age-related slowing', '2025-06-05 13:00:00', '2025-06-05 11:30:00', 1), -- Shifted to current week, scheduled
(89, 4, 5, 'Chronic vomiting', 'Intermittent vomiting episodes.', 'Dietary sensitivity', '2025-06-05 14:00:00', '2025-06-06 10:00:00', 1), -- Shifted to current week, scheduled
(90, 5, 5, 'Respiratory distress', 'Trouble breathing, wheezing.', 'Asthma suspected', '2025-06-06 10:00:00', '2025-06-06 11:00:00', 1), -- Shifted to current week, scheduled
(91, 6, 5, 'Loss of appetite - rabbit', 'Not eating for 24h.', 'Gastrointestinal stasis', '2025-06-06 11:00:00', '2025-06-06 12:00:00', 1), -- Shifted to current week, scheduled
(92, 1, 5, 'Wellness check', 'Routine.', 'Healthy', '2025-06-06 12:00:00', '2025-06-07 09:30:00', 1), -- Shifted to current week, scheduled
(93, 2, 5, 'Dental scaling consult', 'Discussing dental procedure.', 'Scheduled for cleaning', '2025-06-07 09:00:00', '2025-06-07 10:30:00', 1), -- Shifted to current week, scheduled
(94, 3, 5, 'Kidney disease management', 'Monitoring kidney function.', 'Stable', '2025-06-07 10:00:00', '2025-06-07 11:30:00', 1), -- Shifted to current week, scheduled
(95, 4, 5, 'Recheck chronic vomiting', 'Symptoms reduced.', 'Improving', '2025-06-07 11:00:00', '2025-06-09 09:30:00', 1), -- Shifted to future, scheduled
(96, 5, 5, 'Asthma follow-up', 'Breathing easier.', 'Medication effective', '2025-06-08 10:00:00', '2025-06-09 10:30:00', 1), -- Shifted to future, scheduled
(97, 6, 5, 'GI stasis follow-up', 'Rabbit eating small amounts.', 'Slowly recovering', '2025-06-08 11:00:00', '2025-06-09 11:30:00', 1), -- Shifted to future, scheduled
(98, 1, 5, 'Urinary incontinence', 'Leaking urine while sleeping.', 'Weak bladder', '2025-06-08 12:00:00', '2025-06-10 09:30:00', 1), -- Shifted to future, scheduled
(99, 2, 5, 'Eye infection', 'Redness and swelling.', 'Bacterial infection', '2025-06-09 10:00:00', '2025-06-10 10:30:00', 1), -- Shifted to future, scheduled
(100, 3, 5, 'Heart murmur recheck', 'Listening to heart.', 'Stable murmur', '2025-06-09 11:00:00', '2025-06-10 11:30:00', 1), -- Shifted to future, scheduled
(101, 4, 5, 'Annual check-up', 'Routine check-up.', 'Healthy', '2025-06-09 12:00:00', '2025-06-10 12:30:00', 1), -- Shifted to future, scheduled
(102, 5, 5, 'Allergy medication review', 'Discussing current medication.', 'Continue current dosage', '2025-06-10 09:00:00', '2025-06-11 09:30:00', 1), -- Shifted to future, scheduled
(103, 6, 5, 'Dental exam', 'Routine dental check for rabbit.', 'Healthy teeth', '2025-06-10 10:00:00', '2025-06-11 10:30:00', 1), -- Shifted to future, scheduled
(104, 1, 5, 'Pain management consult', 'Discussing options for chronic pain.', 'Started new pain medication', '2025-06-10 11:00:00', '2025-06-11 11:30:00', 1), -- Shifted to future, scheduled
(105, 2, 5, 'Recheck eye infection', 'Eye looks clear.', 'Cleared', '2025-06-10 12:00:00', '2025-06-11 12:30:00', 1), -- Shifted to future, scheduled
(106, 3, 5, 'Dietary advice', 'Owner wants to improve dog diet.', 'Recommended high-quality food', '2025-06-11 09:00:00', '2025-06-11 13:30:00', 1), -- Shifted to future, scheduled
(107, 4, 5, 'Behavioral issue', 'Aggression towards other cats.', 'Referred to behaviorist', '2025-06-11 10:00:00', '2025-06-11 14:30:00', 1), -- Shifted to future, scheduled
(108, 3, 8, 'Allergy retest', 'Checking progress of desensitization.', 'Positive response', '2025-06-04 12:00:00', '2025-06-05 10:00:00', 1), -- Shifted to current week, scheduled
(109, 4, 8, 'Dermatitis follow-up', 'Skin less inflamed.', 'Improving, continue treatment', '2025-06-05 13:00:00', '2025-06-05 11:00:00', 1), -- Shifted to current week, scheduled
(110, 5, 8, 'Chronic itching', 'Ongoing problem.', 'Needs further investigation', '2025-06-05 14:00:00', '2025-06-05 12:00:00', 1), -- Shifted to current week, scheduled
(111, 6, 8, 'Skin infection', 'Redness and scabs.', 'Bacterial skin infection', '2025-06-05 15:00:00', '2025-06-06 10:30:00', 1), -- Shifted to current week, scheduled
(112, 1, 8, 'Hair loss patches', 'Circular patches, suspect ringworm.', 'Fungal infection confirmed', '2025-06-06 11:00:00', '2025-06-06 11:30:00', 1), -- Shifted to current week, scheduled
(113, 2, 8, 'Excessive grooming', 'Licking belly until bald.', 'Behavioral, stress-related', '2025-06-06 12:00:00', '2025-06-06 12:30:00', 1), -- Shifted to current week, scheduled
(114, 3, 8, 'Rash on belly', 'Red bumps.', 'Contact dermatitis', '2025-06-06 13:00:00', '2025-06-07 10:00:00', 1), -- Shifted to current week, scheduled
(115, 4, 8, 'Skin scraping', 'Diagnostic test for mites.', 'Negative for mites', '2025-06-07 09:00:00', '2025-06-07 11:00:00', 1), -- Shifted to current week, scheduled
(116, 5, 8, 'Food allergy test', 'Starting elimination diet.', 'Instruction for owner', '2025-06-07 10:00:00', '2025-06-07 12:00:00', 1), -- Shifted to current week, scheduled
(117, 6, 8, 'Dandruff and dry skin', 'Flaky coat.', 'Needs moisturizing shampoo', '2025-06-07 11:00:00', '2025-06-09 10:00:00', 1), -- Shifted to future, scheduled
(118, 1, 8, 'Ringworm recheck', 'Lesions improving.', 'Healing well', '2025-06-08 10:00:00', '2025-06-09 11:00:00', 1), -- Shifted to future, scheduled
(119, 2, 8, 'Behavioral grooming follow-up', 'Less licking observed.', 'Progressing', '2025-06-08 11:00:00', '2025-06-09 12:00:00', 1), -- Shifted to future, scheduled
(120, 3, 8, 'Recheck contact dermatitis', 'Rash clearing.', 'Almost clear', '2025-06-08 12:00:00', '2025-06-10 10:00:00', 1), -- Shifted to future, scheduled
(121, 4, 8, 'Allergy medication adjustment', 'Increasing dosage.', 'Monitoring response', '2025-06-09 10:00:00', '2025-06-10 11:00:00', 1), -- Shifted to future, scheduled
(122, 5, 8, 'Chronic ear infections', 'Recurring issue.', 'Referred to specialist', '2025-06-09 11:00:00', '2025-06-10 12:00:00', 1), -- Shifted to future, scheduled
(123, 6, 8, 'Foot pad irritation', 'Redness and swelling on paws.', 'Environmental irritant', '2025-06-09 12:00:00', '2025-06-10 13:00:00', 1), -- Shifted to future, scheduled
(124, 1, 8, 'Annual skin check', 'Routine.', 'Healthy skin', '2025-06-10 10:00:00', '2025-06-11 10:00:00', 1), -- Shifted to future, scheduled
(125, 2, 8, 'New skin tags', 'Several small growths.', 'Benign', '2025-06-10 11:00:00', '2025-06-11 11:00:00', 1), -- Shifted to future, scheduled
(126, 3, 8, 'Folliculitis', 'Bumps and hair loss.', 'Bacterial infection', '2025-06-10 12:00:00', '2025-06-11 12:00:00', 1), -- Shifted to future, scheduled
(127, 4, 8, 'Suspected flea allergy', 'Itching around tail base.', 'Confirmed flea allergy', '2025-06-10 13:00:00', '2025-06-11 13:00:00', 1), -- Shifted to future, scheduled
(128, 5, 8, 'General skin health advice', 'Owner seeking preventative tips.', 'Advised on diet and grooming', '2025-06-11 09:00:00', '2025-06-11 14:00:00', 1), -- Shifted to future, scheduled
(129, 6, 8, 'Ulcerated skin lesion', 'Sore on leg.', 'Needs topical treatment', '2025-06-11 10:00:00', '2025-06-11 15:00:00', 1), -- Shifted to future, scheduled
(130, 1, 4, 'Dental cleaning consult', 'Discussing dental procedure.', 'Scheduled for cleaning', '2025-06-11 10:00:00', '2025-06-12 09:00:00', 1), -- Shifted to future, scheduled
(131, 2, 4, 'Weight check', 'Monitoring progress on diet.', 'Maintaining weight', '2025-06-11 11:00:00', '2025-06-12 10:00:00', 1), -- Shifted to future, scheduled
(132, 3, 4, 'Ear cleaning', 'Routine ear maintenance.', 'Ears clean', '2025-06-11 12:00:00', '2025-06-12 11:00:00', 1), -- Shifted to future, scheduled
(133, 4, 4, 'New pet check-up', 'First visit for adopted cat.', 'Healthy', '2025-06-12 09:00:00', '2025-06-12 12:00:00', 1), -- Shifted to future, scheduled
(134, 5, 4, 'Post-op check', 'Following minor surgery.', 'Healing well', '2025-06-12 10:00:00', '2025-06-13 09:30:00', 1), -- Shifted to future, scheduled
(135, 6, 4, 'Vaccination', 'Routine.', 'Vaccinated', '2025-06-12 11:00:00', '2025-06-13 10:30:00', 1), -- Shifted to future, scheduled
(136, 1, 4, 'Follow-up GI upset', 'Symptoms resolved.', 'Fully recovered', '2025-06-12 12:00:00', '2025-06-13 11:30:00', 1), -- Shifted to future, scheduled
(137, 2, 4, 'Annual check-up', 'Routine.', 'Healthy', '2025-06-13 09:00:00', '2025-06-14 09:00:00', 1), -- Shifted to future, scheduled
(138, 3, 4, 'Lump check', 'New small lump.', 'Benign', '2025-06-13 10:00:00', '2025-06-14 10:00:00', 1), -- Shifted to future, scheduled
(139, 4, 4, 'Behavioral consultation', 'Excessive meowing.', 'Separation anxiety', '2025-06-13 11:00:00', '2025-06-14 11:00:00', 1), -- Shifted to future, scheduled
(140, 5, 4, 'Recheck limping', 'Dog still slightly limping.', 'Needs more rest', '2025-06-13 12:00:00', '2025-06-16 09:00:00', 1), -- Shifted to future, scheduled
(141, 6, 4, 'Snuffles symptoms', 'Sneezing and runny nose.', 'Bacterial infection', '2025-06-14 09:00:00', '2025-06-16 10:00:00', 1), -- Shifted to future, scheduled
(142, 1, 4, 'Allergy medication refill', 'Ongoing medication.', 'Stable', '2025-06-14 10:00:00', '2025-06-16 11:00:00', 1), -- Shifted to future, scheduled
(143, 2, 4, 'Routine dental check', 'Checking teeth.', 'Healthy teeth', '2025-06-14 11:00:00', '2025-06-17 09:00:00', 1), -- Shifted to future, scheduled
(144, 3, 4, 'Ear infection', 'Itching and discharge.', 'Otitis', '2025-06-14 12:00:00', '2025-06-17 10:00:00', 1), -- Shifted to future, scheduled
(145, 4, 4, 'Vaccination', 'Annual booster.', 'Vaccinated', '2025-06-16 09:00:00', '2025-06-17 11:00:00', 1), -- Shifted to future, scheduled
(146, 5, 4, 'Senior dog wellness', 'Comprehensive exam for older dog.', 'Healthy', '2025-06-16 10:00:00', '2025-06-17 12:00:00', 1), -- Shifted to future, scheduled
(147, 6, 4, 'Weight monitoring', 'Concerned about weight loss.', 'Needs higher calorie diet', '2025-06-16 11:00:00', '2025-06-17 13:00:00', 1), -- Shifted to future, scheduled
(148, 1, 4, 'New pet check-up', 'First visit for new puppy.', 'Healthy', '2025-06-16 12:00:00', '2025-06-18 09:00:00', 1), -- Shifted to future, scheduled
(149, 2, 4, 'Lethargy', 'Cat seems tired.', 'Mild viral infection', '2025-06-17 09:00:00', '2025-06-18 10:00:00', 1), -- Shifted to future, scheduled
(150, 3, 4, 'Post-dental check', 'Checking on recovery.', 'Recovering well', '2025-06-17 10:00:00', '2025-06-18 11:00:00', 1), -- Shifted to future, scheduled
(151, 4, 4, 'Microchipping', 'Routine implantation.', 'Microchipped', '2025-06-17 11:00:00', '2025-06-18 12:00:00', 1), -- Shifted to future, scheduled
(152, 1, 5, 'Follow-up chronic pain', 'Dog seems more comfortable.', 'Medication effective', '2025-06-11 11:00:00', '2025-06-12 09:30:00', 1), -- Shifted to future, scheduled
(153, 2, 5, 'Dental cleaning', 'Performing procedure.', 'Successful cleaning', '2025-06-11 12:00:00', '2025-06-12 10:30:00', 1), -- Shifted to future, scheduled
(154, 3, 5, 'Blood test', 'Routine senior blood panel.', 'Normal', '2025-06-11 13:00:00', '2025-06-12 11:30:00', 1), -- Shifted to future, scheduled
(155, 4, 5, 'Vomiting and diarrhea', 'Acute onset.', 'Gastroenteritis', '2025-06-12 09:00:00', '2025-06-12 12:30:00', 1), -- Shifted to future, scheduled
(156, 5, 5, 'Respiratory follow-up', 'Breathing normal.', 'Stable', '2025-06-12 10:00:00', '2025-06-13 10:00:00', 1), -- Shifted to future, scheduled
(157, 6, 5, 'GI stasis recheck', 'Rabbit eating well now.', 'Recovered', '2025-06-12 11:00:00', '2025-06-13 11:00:00', 1), -- Shifted to future, scheduled
(158, 1, 5, 'X-ray for lameness', 'Investigating cause of limp.', 'Mild arthritis', '2025-06-12 12:00:00', '2025-06-13 12:00:00', 1), -- Shifted to future, scheduled
(159, 2, 5, 'Post-dental check', 'Checking recovery.', 'Recovering well', '2025-06-13 09:00:00', '2025-06-14 09:30:00', 1), -- Shifted to future, scheduled
(160, 3, 5, 'Urinary incontinence recheck', 'Medication seems effective.', 'Improvement', '2025-06-13 10:00:00', '2025-06-14 10:30:00', 1), -- Shifted to future, scheduled
(161, 4, 5, 'Annual check-up', 'Routine check-up.', 'Healthy', '2025-06-13 11:00:00', '2025-06-14 11:30:00', 1), -- Shifted to future, scheduled
(162, 5, 5, 'Cardiac consult', 'New heart murmur detected.', 'Referred to cardiologist', '2025-06-13 12:00:00', '2025-06-16 09:30:00', 1), -- Shifted to future, scheduled
(163, 6, 5, 'Diarrhea', 'Acute onset of diarrhea.', 'Dietary change', '2025-06-14 09:00:00', '2025-06-16 10:30:00', 1), -- Shifted to future, scheduled
(164, 1, 5, 'Allergy symptoms', 'Itching, red skin.', 'Food allergy suspected', '2025-06-14 10:00:00', '2025-06-16 11:30:00', 1), -- Shifted to future, scheduled
(165, 2, 5, 'Weight management', 'Reviewing diet and exercise.', 'Maintaining healthy weight', '2025-06-14 11:00:00', '2025-06-17 09:30:00', 1), -- Shifted to future, scheduled
(166, 3, 5, 'Chronic pain re-evaluation', 'Adjusting medication.', 'More comfortable', '2025-06-14 12:00:00', '2025-06-17 10:30:00', 1), -- Shifted to future, scheduled
(167, 4, 5, 'Dental exam', 'Routine check.', 'Needs cleaning soon', '2025-06-16 09:00:00', '2025-06-17 11:30:00', 1), -- Shifted to future, scheduled
(168, 5, 5, 'Behavioral issue', 'Separation anxiety.', 'Recommended training', '2025-06-16 10:00:00', '2025-06-17 12:30:00', 1), -- Shifted to future, scheduled
(169, 6, 5, 'Respiratory infection', 'Coughing and sneezing.', 'Viral infection', '2025-06-16 11:00:00', '2025-06-17 13:30:00', 1), -- Shifted to future, scheduled
(170, 1, 5, 'Follow-up diarrhea', 'Stools normalized.', 'Recovered', '2025-06-16 12:00:00', '2025-06-18 09:30:00', 1), -- Shifted to future, scheduled
(171, 2, 5, 'Annual vaccination', 'Booster shot.', 'Vaccinated', '2025-06-17 09:00:00', '2025-06-18 10:30:00', 1), -- Shifted to future, scheduled
(172, 3, 5, 'Allergy testing', 'Preparing for test.', 'Scheduled for test', '2025-06-17 10:00:00', '2025-06-18 11:30:00', 1), -- Shifted to future, scheduled
(173, 4, 5, 'Acute lameness', 'Sudden limp on hind leg.', 'Sprain', '2025-06-17 11:00:00', '2025-06-18 12:30:00', 1), -- Shifted to future, scheduled
(174, 1, 8, 'Atopic dermatitis review', 'Checking medication effectiveness.', 'Stable', '2025-06-11 12:00:00', '2025-06-12 10:00:00', 1), -- Shifted to future, scheduled
(175, 2, 8, 'Skin infection recheck', 'Lesions healing.', 'Healing well', '2025-06-11 13:00:00', '2025-06-12 11:00:00', 1), -- Shifted to future, scheduled
(176, 3, 8, 'Alopecia investigation', 'Patches of hair loss.', 'Hormonal imbalance suspected', '2025-06-11 14:00:00', '2025-06-12 12:00:00', 1), -- Shifted to future, scheduled
(177, 4, 8, 'Ear mites recheck', 'No more scratching.', 'Clear', '2025-06-12 09:00:00', '2025-06-12 13:00:00', 1), -- Shifted to future, scheduled
(178, 5, 8, 'Persistent itching', 'Still scratching excessively.', 'Needs deeper investigation', '2025-06-12 10:00:00', '2025-06-13 10:30:00', 1), -- Shifted to future, scheduled
(179, 6, 8, 'Fungal infection follow-up', 'Skin clearing up.', 'Improving', '2025-06-12 11:00:00', '2025-06-13 11:30:00', 1), -- Shifted to future, scheduled
(180, 1, 8, 'New skin growth', 'Small bump on paw.', 'Benign cyst', '2025-06-12 12:00:00', '2025-06-13 12:30:00', 1), -- Shifted to future, scheduled
(181, 2, 8, 'Dandruff management', 'Discussing dietary supplements.', 'Recommended fish oil', '2025-06-13 09:00:00', '2025-06-14 10:00:00', 1), -- Shifted to future, scheduled
(182, 3, 8, 'Skin allergy testing', 'Conducting patch tests.', 'Results in a few days', '2025-06-13 10:00:00', '2025-06-14 11:00:00', 1), -- Shifted to future, scheduled
(183, 4, 8, 'Pustules on skin', 'New flare-up.', 'Bacterial infection', '2025-06-13 11:00:00', '2025-06-14 12:00:00', 1), -- Shifted to future, scheduled
(184, 5, 8, 'Chronic ear infection management', 'Reviewing long-term strategy.', 'Ongoing management', '2025-06-13 12:00:00', '2025-06-16 10:00:00', 1), -- Shifted to future, scheduled
(185, 6, 8, 'Foot pad lesion', 'Not healing well.', 'Needs surgical debridement', '2025-06-14 09:00:00', '2025-06-16 11:00:00', 1), -- Shifted to future, scheduled
(186, 1, 8, 'Flea allergy dermatitis', 'Severe itching.', 'Treated for fleas', '2025-06-14 10:00:00', '2025-06-16 12:00:00', 1), -- Shifted to future, scheduled
(187, 2, 8, 'Skin cytology', 'Examining skin cells under microscope.', 'Yeast overgrowth', '2025-06-14 11:00:00', '2025-06-17 10:00:00', 1), -- Shifted to future, scheduled
(188, 3, 8, 'Alopecia recheck', 'Hair starting to grow back.', 'Improving', '2025-06-14 12:00:00', '2025-06-17 11:00:00', 1), -- Shifted to future, scheduled
(189, 4, 8, 'Environmental allergies', 'Itching increases seasonally.', 'Started immunotherapy', '2025-06-16 09:00:00', '2025-06-17 12:00:00', 1), -- Shifted to future, scheduled
(190, 5, 8, 'Skin tumor consult', 'Discussing biopsy results.', 'Benign', '2025-06-16 10:00:00', '2025-06-17 13:00:00', 1), -- Shifted to future, scheduled
(191, 6, 8, 'Post-op skin lesion', 'Checking surgical site.', 'Healing well', '2025-06-16 11:00:00', '2025-06-17 14:00:00', 1), -- Shifted to future, scheduled
(192, 1, 8, 'New client consult - skin issue', 'First visit for a dog with chronic ear issues.', 'Chronic otitis', '2025-06-16 12:00:00', '2025-06-18 10:00:00', 1), -- Shifted to future, scheduled
(193, 2, 8, 'Yeast infection recheck', 'Skin less red.', 'Clearing', '2025-06-17 09:00:00', '2025-06-18 11:00:00', 1), -- Shifted to future, scheduled
(194, 3, 8, 'Skin maintenance for senior dog', 'Preventative care.', 'Healthy', '2025-06-17 10:00:00', '2025-06-18 12:00:00', 1), -- Shifted to future, scheduled
(195, 4, 8, 'Pruritus investigation', 'Generalized itching.', 'Allergic reaction', '2025-06-17 11:00:00', '2025-06-18 13:00:00', 1), -- Shifted to future, scheduled
(196, 1, 4, 'Annual check-up', 'Routine check-up for dog.', 'Healthy', '2025-06-18 09:00:00', '2025-06-19 09:00:00', 1), -- Shifted to future, scheduled
(197, 2, 4, 'Lethargy recheck', 'Cat more active.', 'Recovering', '2025-06-18 10:00:00', '2025-06-19 10:00:00', 1), -- Shifted to future, scheduled
(198, 3, 4, 'Weight loss consultation', 'Dog losing weight unintentionally.', 'Needs dietary change', '2025-06-18 11:00:00', '2025-06-19 11:00:00', 1), -- Shifted to future, scheduled
(199, 4, 4, 'Vaccination', 'Annual booster.', 'Vaccinated', '2025-06-19 09:00:00', '2025-06-19 12:00:00', 1), -- Shifted to future, scheduled
(200, 5, 4, 'Dental cleaning', 'Performing procedure.', 'Successful cleaning', '2025-06-19 10:00:00', '2025-06-20 09:00:00', 1), -- Shifted to future, scheduled
(201, 6, 4, 'Respiratory follow-up', 'Rabbit breathing better.', 'Improving', '2025-06-19 11:00:00', '2025-06-20 10:00:00', 1), -- Shifted to future, scheduled
(202, 1, 4, 'Post-dental check', 'Checking recovery.', 'Recovering well', '2025-06-19 12:00:00', '2025-06-20 11:00:00', 1), -- Shifted to future, scheduled
(203, 2, 4, 'Ear infection recheck', 'Ears clearing.', 'Clearing', '2025-06-20 09:00:00', '2025-06-20 12:00:00', 1), -- Shifted to future, scheduled
(204, 3, 4, 'Routine check-up', 'Annual visit.', 'Healthy', '2025-06-20 10:00:00', '2025-06-21 09:00:00', 1), -- Shifted to future, scheduled
(205, 4, 4, 'Limping recheck', 'Cat still slightly limping.', 'Needs further observation', '2025-06-20 11:00:00', '2025-06-21 10:00:00', 1), -- Shifted to future, scheduled
(206, 5, 4, 'Behavioral issue', 'Excessive barking.', 'Recommended training', '2025-06-20 12:00:00', '2025-06-21 11:00:00', 1), -- Shifted to future, scheduled
(207, 6, 4, 'Weight check', 'Monitoring rabbit weight.', 'Stable', '2025-06-21 09:00:00', '2025-06-21 12:00:00', 1), -- Shifted to future, scheduled
(208, 1, 5, 'Arthritis recheck', 'Dog more comfortable.', 'Medication effective', '2025-06-18 10:00:00', '2025-06-19 09:30:00', 1), -- Shifted to future, scheduled
(209, 2, 5, 'Urinary issue recheck', 'Symptoms resolved.', 'Cleared', '2025-06-18 11:00:00', '2025-06-19 10:30:00', 1), -- Shifted to future, scheduled
(210, 3, 5, 'Chronic cough follow-up', 'Cough less frequent.', 'Improving', '2025-06-18 12:00:00', '2025-06-19 11:30:00', 1), -- Shifted to future, scheduled
(211, 4, 5, 'Emergency consult', 'Cat ingested something toxic.', 'Inducing vomiting', '2025-06-19 09:00:00', '2025-06-19 12:30:00', 1), -- Shifted to future, scheduled
(212, 5, 5, 'Vaccination', 'Annual booster.', 'Vaccinated', '2025-06-19 10:00:00', '2025-06-20 10:00:00', 1), -- Shifted to future, scheduled
(213, 6, 5, 'Dental exam', 'Routine check.', 'Healthy teeth', '2025-06-19 11:00:00', '2025-06-20 11:00:00', 1), -- Shifted to future, scheduled
(214, 1, 5, 'Follow-up toxic ingestion', 'Dog recovered well.', 'Stable', '2025-06-19 12:00:00', '2025-06-20 12:00:00', 1), -- Shifted to future, scheduled
(215, 2, 5, 'Routine check-up', 'Annual visit.', 'Healthy', '2025-06-20 09:00:00', '2025-06-20 13:00:00', 1), -- Shifted to future, scheduled
(216, 3, 5, 'Kidney disease recheck', 'Monitoring function.', 'Stable', '2025-06-20 10:00:00', '2025-06-21 09:30:00', 1), -- Shifted to future, scheduled
(217, 4, 5, 'Allergy medication review', 'Discussing current medication.', 'Adjusted dosage', '2025-06-20 11:00:00', '2025-06-21 10:30:00', 1), -- Shifted to future, scheduled
(218, 5, 5, 'Senior check-up', 'Comprehensive exam for older dog.', 'Normal for age', '2025-06-20 12:00:00', '2025-06-21 11:30:00', 1), -- Shifted to future, scheduled
(219, 6, 5, 'Behavioral issue', 'Rabbit gnawing on cage.', 'Needs more toys', '2025-06-21 09:00:00', '2025-06-21 12:30:00', 1), -- Shifted to future, scheduled
(220, 1, 8, 'Skin cytology recheck', 'Yeast count reduced.', 'Improving', '2025-06-18 11:00:00', '2025-06-19 10:00:00', 1), -- Shifted to future, scheduled
(221, 2, 8, 'Chronic itching follow-up', 'Still scratching, but less severe.', 'Monitoring', '2025-06-18 12:00:00', '2025-06-19 11:00:00', 1), -- Shifted to future, scheduled
(222, 3, 8, 'Alopecia follow-up', 'Hair growing back well.', 'Recovering', '2025-06-18 13:00:00', '2025-06-19 12:00:00', 1), -- Shifted to future, scheduled
(223, 4, 8, 'Fungal infection recheck', 'Skin clear.', 'Cleared', '2025-06-19 10:00:00', '2025-06-19 13:00:00', 1), -- Shifted to future, scheduled
(224, 5, 8, 'Allergy injection', 'Routine immunotherapy shot.', 'Administered', '2025-06-19 11:00:00', '2025-06-20 10:30:00', 1), -- Shifted to future, scheduled
(225, 6, 8, 'Foot pad lesion recheck', 'Wound healing well.', 'Healing', '2025-06-19 12:00:00', '2025-06-20 11:30:00', 1), -- Shifted to future, scheduled
(226, 1, 8, 'New client consult - skin issue', 'First visit for dog with chronic ear issues.', 'Chronic otitis', '2025-06-19 13:00:00', '2025-06-20 12:30:00', 1), -- Shifted to future, scheduled
(227, 2, 8, 'Dermatitis follow-up', 'Skin calming.', 'Improving', '2025-06-20 10:00:00', '2025-06-20 13:30:00', 1), -- Shifted to future, scheduled
(228, 3, 8, 'Skin mass consult', 'Discussing removal options.', 'Scheduled for surgery', '2025-06-20 11:00:00', '2025-06-21 10:00:00', 1), -- Shifted to future, scheduled
(229, 4, 8, 'Flea allergy dermatitis', 'Still some itching.', 'Adjusted flea control', '2025-06-20 12:00:00', '2025-06-21 11:00:00', 1), -- Shifted to future, scheduled
(230, 5, 8, 'General skin health advice', 'Owner seeking preventative tips.', 'Advised on diet and grooming', '2025-06-20 13:00:00', '2025-06-21 12:00:00', 1), -- Shifted to future, scheduled
(231, 6, 8, 'Pruritus investigation', 'Generalized itching.', 'Environmental allergy', '2025-06-21 09:00:00', '2025-06-21 13:00:00', 1); -- Shifted to future, scheduled

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `appointmentservices`
--

CREATE TABLE `appointmentservices` (
  `id` int(11) NOT NULL,
  `appointment_id` int(11) DEFAULT NULL,
  `service_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Dumping data for table `appointmentservices`
--

INSERT INTO `appointmentservices` (`id`, `appointment_id`, `service_id`) VALUES
(1, 1, 1),
(2, 2, 3),
(3, 3, 1),
(4, 4, 5),
(5, 5, 1),
(6, 6, 1),
(7, 7, 1),
(8, 8, 1),
(9, 9, 2),
(10, 10, 1),
(11, 11, 1),
(12, 12, 5),
(13, 13, 3),
(14, 14, 1),
(15, 15, 3),
(16, 16, 3),
(17, 17, 3),
(18, 18, 6),
(19, 19, 3),
(20, 20, 1),
(21, 21, 1),
(22, 22, 1),
(23, 23, 1),
(24, 24, 3),
(25, 25, 3),
(26, 26, 3),
(27, 27, 3),
(28, 28, 3),
(29, 29, 3),
(30, 30, 1),
(31, 31, 7),
(32, 32, 1),
(33, 33, 1),
(34, 34, 1),
(35, 35, 3),
(36, 36, 5),
(37, 37, 2),
(38, 38, 1),
(39, 39, 3),
(40, 40, 1),
(41, 41, 1),
(42, 42, 1),
(43, 43, 1),
(44, 44, 8),
(45, 45, 1),
(46, 46, 1),
(47, 47, 1),
(48, 48, 1),
(49, 49, 1),
(50, 50, 1),
(51, 51, 1),
(52, 52, 1),
(53, 53, 8),
(54, 54, 1),
(55, 55, 1),
(56, 56, 1),
(57, 57, 1),
(58, 58, 1),
(59, 59, 1),
(60, 60, 1),
(61, 61, 1),
(62, 62, 1),
(63, 63, 1),
(64, 64, 2),
(65, 65, 1),
(66, 66, 5),
(67, 67, 4),
(68, 68, 3),
(69, 69, 1),
(70, 70, 2),
(71, 71, 1),
(72, 72, 1),
(73, 73, 1),
(74, 74, 3),
(75, 75, 1),
(76, 76, 1),
(77, 77, 5),
(78, 78, 3),
(79, 79, 3),
(80, 80, 1),
(81, 81, 1),
(82, 82, 1),
(83, 83, 1),
(84, 84, 1),
(85, 85, 1),
(86, 86, 7),
(87, 87, 1),
(88, 88, 1),
(89, 89, 3),
(90, 90, 3),
(91, 91, 3),
(92, 92, 1),
(93, 93, 2),
(94, 94, 1),
(95, 95, 1),
(96, 96, 1),
(97, 97, 1),
(98, 98, 3),
(99, 99, 3),
(100, 100, 1),
(101, 101, 1),
(102, 102, 1),
(103, 103, 2),
(104, 104, 1),
(105, 105, 1),
(106, 106, 1),
(107, 107, 1),
(108, 108, 8),
(109, 109, 1),
(110, 110, 1),
(111, 111, 1),
(112, 112, 1),
(113, 113, 1),
(114, 114, 1),
(115, 115, 1),
(116, 116, 8),
(117, 117, 1),
(118, 118, 1),
(119, 119, 1),
(120, 120, 1),
(121, 121, 1),
(122, 122, 1),
(123, 123, 1),
(124, 124, 1),
(125, 125, 1),
(126, 126, 1),
(127, 127, 1),
(128, 128, 1),
(129, 129, 1),
(130, 130, 2),
(131, 131, 1),
(132, 132, 1),
(133, 133, 1),
(134, 134, 1),
(135, 135, 5),
(136, 136, 1),
(137, 137, 1),
(138, 138, 1),
(139, 139, 1),
(140, 140, 1),
(141, 141, 3),
(142, 142, 1),
(143, 143, 2),
(144, 144, 3),
(145, 145, 5),
(146, 146, 1),
(147, 147, 1),
(148, 148, 1),
(149, 149, 3),
(150, 150, 1),
(151, 151, 1),
(152, 152, 1),
(153, 153, 2),
(154, 154, 7),
(155, 155, 3),
(156, 156, 1),
(157, 157, 1),
(158, 158, 6),
(159, 159, 1),
(160, 160, 1),
(161, 161, 1),
(162, 162, 1),
(163, 163, 3),
(164, 164, 3),
(165, 165, 1),
(166, 166, 1),
(167, 167, 2),
(168, 168, 1),
(169, 169, 3),
(170, 170, 1),
(171, 171, 5),
(172, 172, 8),
(173, 173, 3),
(174, 174, 1),
(175, 175, 1),
(176, 176, 1),
(177, 177, 1),
(178, 178, 1),
(179, 179, 1),
(180, 180, 1),
(181, 181, 1),
(182, 182, 8),
(183, 183, 1),
(184, 184, 1),
(185, 185, 4),
(186, 186, 1),
(187, 187, 1),
(188, 188, 1),
(189, 189, 1),
(190, 190, 1),
(191, 191, 1),
(192, 192, 1),
(193, 193, 1),
(194, 194, 1),
(195, 195, 1),
(196, 196, 1),
(197, 197, 1),
(198, 198, 1),
(199, 199, 5),
(200, 200, 2),
(201, 201, 1),
(202, 202, 1),
(203, 203, 1),
(204, 204, 1),
(205, 205, 1),
(206, 206, 1),
(207, 207, 1),
(208, 208, 1),
(209, 209, 1),
(210, 210, 1),
(211, 211, 3),
(212, 212, 5),
(213, 213, 2),
(214, 214, 1),
(215, 215, 1),
(216, 216, 1),
(217, 217, 1),
(218, 218, 1),
(219, 219, 1),
(220, 220, 1),
(221, 221, 1),
(222, 222, 1),
(223, 223, 1),
(224, 224, 1),
(225, 225, 1),
(226, 226, 1),
(227, 227, 1),
(228, 228, 4),
(229, 229, 1),
(230, 230, 1),
(231, 231, 1);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `appointmentstatus`
--

CREATE TABLE `appointmentstatus` (
  `id` int(11) NOT NULL,
  `status` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `appointmentstatus`
--

INSERT INTO `appointmentstatus` (`id`, `status`) VALUES
(1, 'Scheduled'),
(2, 'In progress'),
(3, 'Completed'),
(4, 'Canceled');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `client`
--

CREATE TABLE `client` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `surname` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Dumping data for table `client`
--

INSERT INTO `client` (`id`, `name`, `surname`) VALUES
(2, 'Jan', 'Kowalski'),
(3, 'Anna', 'Nowak'),
(6, 'Piotr', 'Zielinski'),
(7, 'Agata', 'W?jcik');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `doctor`
--

CREATE TABLE `doctor` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `surname` varchar(255) NOT NULL,
  `specialization` varchar(255) NOT NULL,
  `description` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Dumping data for table `doctor`
--

INSERT INTO `doctor` (`id`, `name`, `surname`, `specialization`, `description`) VALUES
(4, 'Maria', 'Smith', 'Small Animal Surgery', 'Experienced surgeon with a passion for pets.'),
(5, 'David', 'Jones', 'Internal Medicine', 'Specializes in diagnosing and treating complex diseases.'),
(8, 'Olivia', 'Brown', 'Dermatology', 'Expert in skin conditions and allergies.');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `drug`
--

CREATE TABLE `drug` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `dosage_form` enum('Tablet','Liquid','Injection') NOT NULL,
  `strength` varchar(255) NOT NULL,
  `unit_of_measure` varchar(255) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `manufacturer` varchar(255) NOT NULL,
  `stock_quantity` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Dumping data for table `drug`
--

INSERT INTO `drug` (`id`, `name`, `dosage_form`, `strength`, `unit_of_measure`, `description`, `manufacturer`, `stock_quantity`) VALUES
(1, 'Amoxicillin', 'Tablet', '250mg', 'mg', 'Broad-spectrum antibiotic', 'PharmaCo', 500),
(2, 'Meloxicam', 'Liquid', '1.5mg/ml', 'ml', 'NSAID for pain and inflammation', 'VetMed Labs', 200),
(3, 'Flea & Tick Prevention', 'Tablet', '1 tablet', 'tablet', 'Monthly preventative treatment', 'PetHealth Inc.', 100),
(4, 'Hydrocortisone Spray', 'Liquid', '1%', 'ml', 'Topical steroid for skin irritation', 'DermVet Pharma', 150),
(5, 'Gabapentin', '', '100mg', 'mg', 'Pain relief and anti-anxiety', 'NeuroVet', 300),
(6, 'Probiotic', '', '5 billion CFU', 'sachet', 'Supports digestive health', 'GutHealth Co.', 250);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `opinion`
--

CREATE TABLE `opinion` (
  `id` int(11) NOT NULL,
  `client_id` int(11) NOT NULL,
  `doctor_id` int(11) NOT NULL,
  `comment` varchar(255) NOT NULL,
  `rating` smallint(6) NOT NULL,
  `created_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Dumping data for table `opinion`
--

INSERT INTO `opinion` (`id`, `client_id`, `doctor_id`, `comment`, `rating`, `created_at`) VALUES
(1, 2, 4, 'Dr. Smith was excellent, very thorough with Burek.', 5, '2025-05-23 11:00:00'),
(2, 3, 4, 'Azor received great care. Very professional and friendly.', 4, '2025-05-25 10:00:00'),
(3, 2, 5, 'Dr. Jones was helpful but the cat is still recovering.', 3, '2025-05-24 16:00:00'),
(4, 6, 4, 'Dr. Smith was fantastic with Kropka\'s check-up. Highly recommend!', 5, '2025-05-02 10:30:00'),
(5, 7, 5, 'Dr. Jones helped Max with his allergies. Very knowledgeable.', 4, '2025-05-02 11:30:00'),
(6, 2, 8, 'Dr. Brown quickly diagnosed Burek\'s skin issue. Great service.', 5, '2025-05-01 10:30:00'),
(7, 3, 4, 'Excellent dental cleaning for Azor. So grateful!', 5, '2025-05-10 10:30:00'),
(8, 6, 5, 'Dr. Jones was patient with Rysiek\'s GI issues. Good follow-up.', 4, '2025-05-09 12:30:00'),
(9, 7, 8, 'Olivia Brown is the best dermatologist! Max\'s skin is much better.', 5, '2025-05-05 11:30:00'),
(10, 2, 4, 'Another smooth vaccination visit with Dr. Smith.', 4, '2025-05-07 10:30:00'),
(11, 3, 5, 'Dr. Jones identified Azor\'s UTI quickly. Professional and caring.', 5, '2025-05-03 11:00:00'),
(12, 6, 8, 'Rysiek\'s fungal infection is clearing up, thanks to Dr. Brown.', 4, '2025-05-07 15:30:00'),
(13, 7, 4, 'Max\'s post-op recovery is going well. Dr. Smith is very attentive.', 5, '2025-05-08 10:30:00'),
(14, 2, 5, 'Burek\'s chronic pain is much better managed now. Thank you, Dr. Jones.', 5, '2025-05-15 10:00:00'),
(15, 3, 8, 'Dr. Brown is thorough with skin checks. Azor had a minor growth removed.', 4, '2025-05-15 12:30:00'),
(16, 6, 4, 'Kropka\'s annual check-up was quick and efficient.', 4, '2025-05-17 11:30:00'),
(17, 7, 5, 'Dr. Jones handled Max\'s emergency consultation very well.', 5, '2025-05-22 13:00:00'),
(18, 2, 8, 'Burek\'s chronic ear issues are finally under control thanks to Dr. Brown.', 5, '2025-05-21 10:30:00'),
(19, 3, 4, 'Azor\'s diet change was recommended by Dr. Smith and is helping with his weight.', 4, '2025-05-22 11:30:00'),
(20, 6, 5, 'Rysiek needed more toys for his behavior, good advice from Dr. Jones.', 3, '2025-05-24 13:00:00'),
(21, 7, 8, 'Olivia Brown is incredibly detailed in her skin assessments. Max is in good hands.', 5, '2025-05-24 12:30:00');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `pet`
--

CREATE TABLE `pet` (
  `id` int(11) NOT NULL,
  `client_id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `species` varchar(255) NOT NULL,
  `breed` varchar(255) DEFAULT NULL,
  `weight` decimal(10,0) NOT NULL,
  `gender` enum('Male','Female','Neutered','Spayed') NOT NULL,
  `date_of_birth` date NOT NULL,
  `created_at` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Dumping data for table `pet`
--

INSERT INTO `pet` (`id`, `client_id`, `name`, `species`, `breed`, `weight`, `gender`, `date_of_birth`, `created_at`) VALUES
(1, 2, 'Burek', 'Dog', 'German Shepherd', 35, 'Male', '2020-05-10', '2024-01-10 15:00:00'),
(2, 2, 'Mruczek', 'Cat', 'Siamese', 5, 'Neutered', '2021-02-15', '2024-01-10 15:05:00'),
(3, 3, 'Azor', 'Dog', 'Golden Retriever', 28, 'Spayed', '2019-08-01', '2024-01-12 09:00:00'),
(4, 6, 'Kropka', 'Cat', 'Domestic Shorthair', 4, 'Female', '2022-03-20', '2024-03-05 16:00:00'),
(5, 7, 'Max', 'Dog', 'Labrador', 30, 'Male', '2018-06-01', '2024-03-18 10:30:00'),
(6, 6, 'Rysiek', 'Rabbit', 'Dutch', 2, 'Male', '2023-01-01', '2024-04-01 11:00:00');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `prescription`
--

CREATE TABLE `prescription` (
  `id` int(11) NOT NULL,
  `appointment_id` int(11) NOT NULL,
  `instructions` varchar(255) NOT NULL,
  `expiry_date` datetime NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Dumping data for table `prescription`
--

INSERT INTO `prescription` (`id`, `appointment_id`, `instructions`, `expiry_date`, `created_at`) VALUES
(1, 2, 'Administer with food twice daily for 7 days.', '2025-06-10 00:00:00', '2025-05-23 15:00:00'),
(2, 3, 'Give orally once daily for 5 days. Re-evaluate after X-ray results.', '2025-06-15 00:00:00', '2025-05-24 12:00:00'),
(3, 6, 'Administer 1 tablet orally twice daily for 10 days.', '2025-06-15 00:00:00', '2025-05-01 11:30:00'),
(4, 24, 'Administer 2ml orally twice daily for 5 days.', '2025-06-10 00:00:00', '2025-05-01 10:00:00'),
(5, 25, 'Administer 1 tablet orally once daily for 7 days.', '2025-06-12 00:00:00', '2025-05-01 11:00:00'),
(6, 44, 'Spray affected area twice daily.', '2025-06-15 00:00:00', '2025-05-01 10:30:00'),
(7, 45, 'Apply cream to hot spots daily.', '2025-06-16 00:00:00', '2025-05-01 11:30:00'),
(8, 16, 'Administer 1ml orally once daily for 3 days.', '2025-06-08 00:00:00', '2025-05-06 09:30:00'),
(9, 28, 'Apply spray twice daily to affected areas for 7 days.', '2025-06-10 00:00:00', '2025-05-02 11:30:00'),
(10, 48, 'Apply topical antibiotic twice daily for 10 days.', '2025-06-18 00:00:00', '2025-05-02 12:00:00'),
(11, 55, 'Apply antifungal cream twice daily for 14 days.', '2025-06-19 00:00:00', '2025-05-05 12:30:00'),
(12, 68, 'Administer 0.5ml orally twice daily for 7 days.', '2025-06-20 00:00:00', '2025-05-09 11:00:00'),
(13, 79, 'Administer 0.2ml orally once daily for 5 days.', '2025-06-21 00:00:00', '2025-05-13 12:30:00'),
(14, 90, 'Administer 1 tablet orally twice daily for 10 days.', '2025-06-22 00:00:00', '2025-05-09 11:30:00'),
(15, 111, 'Apply topical antibiotic twice daily for 7 days.', '2025-06-23 00:00:00', '2025-05-09 11:00:00'),
(16, 112, 'Administer 1 tablet orally once daily for 14 days.', '2025-06-24 00:00:00', '2025-05-09 12:00:00'),
(17, 141, 'Administer 0.3ml orally twice daily for 7 days.', '2025-07-01 00:00:00', '2025-05-19 10:30:00'),
(18, 144, 'Apply ear drops twice daily for 7 days.', '2025-07-02 00:00:00', '2025-05-20 10:30:00'),
(19, 155, 'Administer 1 sachet orally once daily for 5 days.', '2025-07-03 00:00:00', '2025-05-15 13:00:00'),
(20, 169, 'Administer 0.4ml orally twice daily for 7 days.', '2025-07-04 00:00:00', '2025-05-20 14:00:00'),
(21, 183, 'Apply topical antibiotic twice daily for 7 days.', '2025-07-05 00:00:00', '2025-05-17 12:30:00'),
(22, 187, 'Administer 1 sachet orally once daily for 10 days.', '2025-07-06 00:00:00', '2025-05-20 10:30:00'),
(23, 211, 'Administer 1 tablet orally once.', '2025-07-07 00:00:00', '2025-05-22 13:00:00');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `prescriptiondrugs`
--

CREATE TABLE `prescriptiondrugs` (
  `id` int(11) NOT NULL,
  `prescription_id` int(11) NOT NULL,
  `drug_id` int(11) NOT NULL,
  `quantity` decimal(10,0) NOT NULL,
  `dosage_instructions` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Dumping data for table `prescriptiondrugs`
--

INSERT INTO `prescriptiondrugs` (`id`, `prescription_id`, `drug_id`, `quantity`, `dosage_instructions`) VALUES
(1, 1, 1, 14, 'One 250mg tablet, twice daily with food.'),
(2, 2, 2, 50, '2 ml orally once daily for 5 days.'),
(3, 3, 1, 20, 'One 250mg tablet, twice daily.'),
(4, 4, 2, 10, '2ml orally twice daily.'),
(5, 5, 1, 7, 'One 250mg tablet, once daily.'),
(6, 6, 4, 1, 'Spray affected area twice daily.'),
(7, 7, 4, 1, 'Apply to hot spots daily.'),
(8, 8, 2, 3, '1ml orally once daily.'),
(9, 9, 4, 1, 'Spray twice daily to affected areas.'),
(10, 10, 1, 1, 'Apply topical antibiotic twice daily.'),
(11, 11, 4, 1, 'Apply antifungal cream twice daily.'),
(12, 12, 2, 4, '0.5ml orally twice daily.'),
(13, 13, 2, 1, '0.2ml orally once daily.'),
(14, 14, 5, 20, 'One 100mg capsule twice daily.'),
(15, 15, 1, 1, 'Apply topical antibiotic twice daily.'),
(16, 16, 3, 14, 'One tablet orally once daily.'),
(17, 17, 2, 4, '0.3ml orally twice daily.'),
(18, 18, 4, 1, 'Apply ear drops twice daily.'),
(19, 19, 6, 5, 'One sachet orally once daily.'),
(20, 20, 2, 6, '0.4ml orally twice daily.'),
(21, 21, 1, 1, 'Apply topical antibiotic twice daily.'),
(22, 22, 6, 10, 'One sachet orally once daily.'),
(23, 23, 1, 1, 'One tablet orally once.');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `role`
--

CREATE TABLE `role` (
  `id` int(11) NOT NULL,
  `name` varchar(6) NOT NULL DEFAULT 'client',
  `description` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Dumping data for table `role`
--

INSERT INTO `role` (`id`, `name`, `description`) VALUES
(1, 'admin', 'Administrator with full system access.'),
(2, 'client', 'Standard client with access to their own pets and appointments.'),
(3, 'doctor', 'Veterinarian with access to appointments and patient records.');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `service`
--

CREATE TABLE `service` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL,
  `price` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Dumping data for table `service`
--

INSERT INTO `service` (`id`, `name`, `description`, `price`) VALUES
(1, 'Routine Check-up', 'Annual health examination and vaccinations.', 100),
(2, 'Dental Cleaning', 'Professional dental scaling and polishing.', 250),
(3, 'Emergency Consultation', 'Urgent medical assessment for critical conditions.', 150),
(4, 'Surgery - Spay/Neuter', 'Surgical procedure for sterilization.', 400),
(5, 'Vaccination', 'Administration of necessary vaccines.', 75),
(6, 'X-ray', 'Diagnostic imaging using X-rays.', 120),
(7, 'Blood Test', 'Comprehensive blood work for diagnostic purposes.', 90),
(8, 'Allergy Testing', 'Tests to identify specific allergens in pets.', 180);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `user`
--

CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `role_id` int(11) DEFAULT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `gender` enum('Female','Male') NOT NULL,
  `telephone` varchar(20) NOT NULL,
  `date_of_birth` date NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `last_login` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `role_id`, `email`, `password`, `gender`, `telephone`, `date_of_birth`, `created_at`, `last_login`) VALUES
(1, 1, 'admin@vetclinic.com', 'adminpass123', 'Male', '111222333', '1985-01-15', '2024-01-01 10:00:00', '2025-05-21 21:55:00'),
(2, 2, 'jan.kowalski@email.com', 'clientpass123', 'Male', '444555666', '1990-03-20', '2024-01-05 11:30:00', '2025-05-21 22:00:00'),
(3, 2, 'anna.nowak@email.com', 'clientpass456', 'Female', '777888999', '1992-07-25', '2024-01-10 14:00:00', '2025-05-21 22:02:00'),
(4, 3, 'dr.smith@vetclinic.com', 'doctorpass123', 'Female', '123123123', '1980-04-10', '2024-02-01 09:00:00', '2025-05-21 22:03:00'),
(5, 3, 'dr.jones@vetclinic.com', 'doctorpass456', 'Male', '321321321', '1975-11-01', '2024-02-05 09:30:00', '2025-05-21 22:04:00'),
(6, 2, 'piotr.zielinski@email.com', 'clientpass789', 'Male', '987654321', '1988-11-05', '2024-03-01 10:00:00', '2025-05-21 22:05:00'),
(7, 2, 'agata.w?jcik@email.com', 'clientpassabc', 'Female', '654987321', '1995-09-12', '2024-03-15 11:00:00', '2025-05-21 22:06:00'),
(8, 3, 'dr.brown@vetclinic.com', 'doctorpass789', 'Female', '456789012', '1978-06-20', '2024-04-01 08:30:00', '2025-05-21 22:07:00');

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `admin`
--
ALTER TABLE `admin`
  ADD KEY `fk_admin_user` (`id`);

--
-- Indeksy dla tabeli `appointment`
--
ALTER TABLE `appointment`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_appointment_pet` (`pet_id`),
  ADD KEY `fk_appointment_doctor` (`doctor_id`),
  ADD KEY `fk_appointment_status` (`status_id`);

--
-- Indeksy dla tabeli `appointmentservices`
--
ALTER TABLE `appointmentservices`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_appointment_service` (`appointment_id`),
  ADD KEY `fk_service_appointment` (`service_id`);

--
-- Indeksy dla tabeli `appointmentstatus`
--
ALTER TABLE `appointmentstatus`
  ADD PRIMARY KEY (`id`);

--
-- Indeksy dla tabeli `client`
--
ALTER TABLE `client`
  ADD PRIMARY KEY (`id`);

--
-- Indeksy dla tabeli `doctor`
--
ALTER TABLE `doctor`
  ADD PRIMARY KEY (`id`);

--
-- Indeksy dla tabeli `drug`
--
ALTER TABLE `drug`
  ADD PRIMARY KEY (`id`);

--
-- Indeksy dla tabeli `opinion`
--
ALTER TABLE `opinion`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_opinion_client` (`client_id`),
  ADD KEY `fk_opinion_doctor` (`doctor_id`);

--
-- Indeksy dla tabeli `pet`
--
ALTER TABLE `pet`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_pet_client` (`client_id`);

--
-- Indeksy dla tabeli `prescription`
--
ALTER TABLE `prescription`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_prescription_appointment` (`appointment_id`);

--
-- Indeksy dla tabeli `prescriptiondrugs`
--
ALTER TABLE `prescriptiondrugs`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_drug_prescription` (`drug_id`),
  ADD KEY `fk_prescription_dug` (`prescription_id`);

--
-- Indeksy dla tabeli `role`
--
ALTER TABLE `role`
  ADD PRIMARY KEY (`id`);

--
-- Indeksy dla tabeli `service`
--
ALTER TABLE `service`
  ADD PRIMARY KEY (`id`);

--
-- Indeksy dla tabeli `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD KEY `fk_user_role` (`role_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `appointment`
--
ALTER TABLE `appointment`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=232;

--
-- AUTO_INCREMENT for table `appointmentservices`
--
ALTER TABLE `appointmentservices`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=232;

--
-- AUTO_INCREMENT for table `appointmentstatus`
--
ALTER TABLE `appointmentstatus`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `drug`
--
ALTER TABLE `drug`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `opinion`
--
ALTER TABLE `opinion`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT for table `pet`
--
ALTER TABLE `pet`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `prescription`
--
ALTER TABLE `prescription`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT for table `prescriptiondrugs`
--
ALTER TABLE `prescriptiondrugs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT for table `role`
--
ALTER TABLE `role`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `service`
--
ALTER TABLE `service`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `admin`
--
ALTER TABLE `admin`
  ADD CONSTRAINT `fk_admin_user` FOREIGN KEY (`id`) REFERENCES `user` (`id`);

--
-- Constraints for table `appointment`
--
ALTER TABLE `appointment`
  ADD CONSTRAINT `fk_appointment_doctor` FOREIGN KEY (`doctor_id`) REFERENCES `doctor` (`id`),
  ADD CONSTRAINT `fk_appointment_pet` FOREIGN KEY (`pet_id`) REFERENCES `pet` (`id`),
  ADD CONSTRAINT `fk_appointment_status` FOREIGN KEY (`status_id`) REFERENCES `appointmentstatus` (`id`);

--
-- Constraints for table `appointmentservices`
--
ALTER TABLE `appointmentservices`
  ADD CONSTRAINT `fk_appointment_service` FOREIGN KEY (`appointment_id`) REFERENCES `appointment` (`id`),
  ADD CONSTRAINT `fk_service_appointment` FOREIGN KEY (`service_id`) REFERENCES `service` (`id`);

--
-- Constraints for table `client`
--
ALTER TABLE `client`
  ADD CONSTRAINT `fk_client_user` FOREIGN KEY (`id`) REFERENCES `user` (`id`);

--
-- Constraints for table `doctor`
--
ALTER TABLE `doctor`
  ADD CONSTRAINT `fk_doctor_user` FOREIGN KEY (`id`) REFERENCES `user` (`id`);

--
-- Constraints for table `opinion`
--
ALTER TABLE `opinion`
  ADD CONSTRAINT `fk_opinion_client` FOREIGN KEY (`client_id`) REFERENCES `client` (`id`),
  ADD CONSTRAINT `fk_opinion_doctor` FOREIGN KEY (`doctor_id`) REFERENCES `doctor` (`id`);

--
-- Constraints for table `pet`
--
ALTER TABLE `pet`
  ADD CONSTRAINT `fk_pet_client` FOREIGN KEY (`client_id`) REFERENCES `client` (`id`);

--
-- Constraints for table `prescription`
--
ALTER TABLE `prescription`
  ADD CONSTRAINT `fk_prescription_appointment` FOREIGN KEY (`appointment_id`) REFERENCES `appointment` (`id`);

--
-- Constraints for table `prescriptiondrugs`
--
ALTER TABLE `prescriptiondrugs`
  ADD CONSTRAINT `fk_drug_prescription` FOREIGN KEY (`drug_id`) REFERENCES `drug` (`id`),
  ADD CONSTRAINT `fk_prescription_dug` FOREIGN KEY (`prescription_id`) REFERENCES `prescription` (`id`);

--
-- Constraints for table `user`
--
ALTER TABLE `user`
  ADD CONSTRAINT `fk_user_role` FOREIGN KEY (`role_id`) REFERENCES `role` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
