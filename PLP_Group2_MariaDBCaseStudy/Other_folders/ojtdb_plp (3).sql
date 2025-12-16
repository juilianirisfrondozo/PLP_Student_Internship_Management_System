-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 12, 2025 at 09:41 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `ojtdb_plp`
--

-- --------------------------------------------------------

--
-- Table structure for table `analytics`
--

CREATE TABLE `analytics` (
  `id` int(11) NOT NULL,
  `student_last_name` varchar(100) NOT NULL,
  `student_first_name` varchar(100) NOT NULL,
  `gender` varchar(10) NOT NULL,
  `course` varchar(100) NOT NULL,
  `department` varchar(100) NOT NULL,
  `section` varchar(50) NOT NULL,
  `student_email` varchar(150) NOT NULL,
  `student_contact_no` varchar(20) NOT NULL,
  `q1` tinyint(4) NOT NULL CHECK (`q1` between 1 and 5),
  `q2` tinyint(4) NOT NULL CHECK (`q2` between 1 and 5),
  `q3` tinyint(4) NOT NULL CHECK (`q3` between 1 and 5),
  `q4` tinyint(4) NOT NULL CHECK (`q4` between 1 and 5),
  `q5` tinyint(4) NOT NULL CHECK (`q5` between 1 and 5),
  `q6` tinyint(4) NOT NULL CHECK (`q6` between 1 and 5),
  `q7` tinyint(4) NOT NULL CHECK (`q7` between 1 and 5),
  `q8` tinyint(4) NOT NULL CHECK (`q8` between 1 and 5),
  `q9` tinyint(4) NOT NULL CHECK (`q9` between 1 and 5),
  `q10` tinyint(4) NOT NULL CHECK (`q10` between 1 and 5),
  `q11` tinyint(4) NOT NULL CHECK (`q11` between 1 and 5),
  `q12` tinyint(4) NOT NULL CHECK (`q12` between 1 and 5),
  `q13` tinyint(4) NOT NULL CHECK (`q13` between 1 and 5),
  `q14` tinyint(4) NOT NULL CHECK (`q14` between 1 and 5),
  `q15` tinyint(4) NOT NULL CHECK (`q15` between 1 and 5),
  `q16` tinyint(4) NOT NULL CHECK (`q16` between 1 and 5),
  `q17` tinyint(4) NOT NULL CHECK (`q17` between 1 and 5),
  `q18` tinyint(4) NOT NULL CHECK (`q18` between 1 and 5),
  `q19` tinyint(4) NOT NULL CHECK (`q19` between 1 and 5),
  `q20` tinyint(4) NOT NULL CHECK (`q20` between 1 and 5),
  `total_earned_points` decimal(5,2) NOT NULL CHECK (`total_earned_points` between 1 and 100.00),
  `rating` decimal(2,1) NOT NULL CHECK (`rating` between 1 and 5)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `company`
--

CREATE TABLE `company` (
  `company_id` varchar(10) NOT NULL,
  `company_name` varchar(120) NOT NULL,
  `company_address` varchar(200) NOT NULL,
  `industry_type` varchar(50) NOT NULL,
  `company_contact_no` varchar(11) NOT NULL,
  `company_email` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `company`
--

INSERT INTO `company` (`company_id`, `company_name`, `company_address`, `industry_type`, `company_contact_no`, `company_email`) VALUES
('C001', 'TechNation', 'Quezon City', 'Software Application Development', '1111', 'SAD@email.com'),
('C002', 'CyberPower', 'Pasig City', 'Database Administrators', '2222', 'DA@email.com'),
('C003', 'ByteBrige', 'Marikina City', 'Hardware Engineer', '3333', 'CS@email.com'),
('C004', 'NovelTech', 'Mandaluyong City', 'Nurse Practitioner', '4444', 'DAA@email.com'),
('C005', 'Villa', 'Mandaluyong City', 'Business', '1235461212', 'Villa@gmail.com');

-- --------------------------------------------------------

--
-- Table structure for table `company_contact`
--

CREATE TABLE `company_contact` (
  `contact_id` varchar(10) NOT NULL,
  `company_id` varchar(10) NOT NULL,
  `contact_first_name` varchar(120) NOT NULL,
  `contact_last_name` varchar(120) NOT NULL,
  `contact_position` varchar(50) NOT NULL,
  `contact_contact_no` varchar(11) NOT NULL,
  `email` varchar(50) NOT NULL,
  `grade_student` decimal(5,2) DEFAULT NULL CHECK (`grade_student` >= 0.00 and `grade_student` <= 100.00),
  `student_id` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `company_contact`
--

INSERT INTO `company_contact` (`contact_id`, `company_id`, `contact_first_name`, `contact_last_name`, `contact_position`, `contact_contact_no`, `email`, `grade_student`, `student_id`) VALUES
('CC001', 'C001', 'Adrian', 'Dela Cruz', 'Software Engineer', '09171234567', 'Ada@email.com', 97.00, 'S001'),
('CC002', 'C002', 'Bianca', 'Santos', 'Project Manager (IT)', '09284567890', 'Bsa@email.com', 95.00, 'S002'),
('CC003', 'C003', 'Carlo', 'Mendoza', 'Industrial Engineer', '09081239876', 'Cm@email.com', 92.00, 'S003'),
('CC004', 'C004', 'Denise', 'Villanueva', 'Pediatric Nurse', '09364571234', 'Dv@email.com', 94.00, 'S004'),
('CC005', 'C003', 'Garry', 'Lopez', 'Manager', '12374823', 'G@gmail.com', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `course`
--

CREATE TABLE `course` (
  `course_id` varchar(10) NOT NULL,
  `course_name` varchar(120) NOT NULL,
  `department_id` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `course`
--

INSERT INTO `course` (`course_id`, `course_name`, `department_id`) VALUES
('CR001', 'BS Information Technology', 'D001'),
('CR002', 'BS Electronics Engineering', 'D002'),
('CR003', 'BS Nursing', 'D003'),
('CR004', 'BS Computer Science ', 'D001'),
('CR005', 'BS Accountancy ', 'D006'),
('CR006', 'BS Business Administration Major in Marketing Management', 'D006'),
('CR007', 'BS Entrepreneurship', 'D006'),
('CR008', 'BS Hospitality Management', 'D004'),
('CR009', 'Bachelor of Elementary Education', 'D005'),
('CR010', 'Bachelor of Secondary Education Major in English', 'D005'),
('CR011', 'Bachelor of Secondary Education Major in Filipino', 'D005'),
('CR012', 'Bachelor of Secondary Education Major in Mathematics ', 'D005'),
('CR013', 'AB Psychology ', 'D007');

-- --------------------------------------------------------

--
-- Table structure for table `department`
--

CREATE TABLE `department` (
  `department_id` varchar(10) NOT NULL,
  `department_name` varchar(120) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `department`
--

INSERT INTO `department` (`department_id`, `department_name`) VALUES
('D001', 'College of Computer Studies'),
('D002', 'College of Engineering'),
('D003', 'College of Nursing'),
('D004', 'College of Hospitality Management '),
('D005', 'College of Education'),
('D006', 'College of Business And Accountancy'),
('D007', 'College of Arts And Sciences ');

-- --------------------------------------------------------

--
-- Table structure for table `faculty`
--

CREATE TABLE `faculty` (
  `faculty_id` varchar(10) NOT NULL,
  `faculty_first_name` varchar(120) NOT NULL,
  `faculty_last_name` varchar(120) NOT NULL,
  `faculty_position` varchar(50) NOT NULL,
  `department_id` varchar(10) NOT NULL,
  `faculty_contact_no` varchar(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `faculty`
--

INSERT INTO `faculty` (`faculty_id`, `faculty_first_name`, `faculty_last_name`, `faculty_position`, `department_id`, `faculty_contact_no`) VALUES
('F001', 'Ethan Lee', 'Ramos', 'Laboratory Instructor', 'D001', '09198765432'),
('F002', 'Faith', 'Garcia', 'Lecturer', 'D001', '09471239876'),
('F003', 'Gabriel', 'Reyes', 'Professor', 'D002', '09073451298'),
('F004', 'Hannah', 'Lim', 'Professor', 'D003', '09281234567'),
('F005', '', '', '', 'D001', ''),
('F006', 'Rhobie Mae', 'Basalatan', '', 'D004', ''),
('F007', 'John ', 'Enon', '', 'D002', ''),
('F008', 'Tricia', 'Rondolo', '', 'D003', ''),
('F009', 'Iris', 'Frondozo', '', 'D002', ''),
('F010', 'anna', 'delacruz', '', 'D002', ''),
('F011', 'Eve', 'Torres', '', 'D001', ''),
('F012', 'Riva', 'Santos', '', 'D001', '');

-- --------------------------------------------------------

--
-- Table structure for table `faculty_section`
--

CREATE TABLE `faculty_section` (
  `faculty_id` varchar(10) NOT NULL,
  `section_name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `faculty_user_account`
--

CREATE TABLE `faculty_user_account` (
  `user_id` int(11) NOT NULL,
  `email` varchar(100) NOT NULL,
  `first_name` varchar(120) NOT NULL,
  `last_name` varchar(120) NOT NULL,
  `password` varchar(255) NOT NULL,
  `faculty_id` varchar(10) DEFAULT NULL,
  `department_id` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `faculty_user_account`
--

INSERT INTO `faculty_user_account` (`user_id`, `email`, `first_name`, `last_name`, `password`, `faculty_id`, `department_id`) VALUES
(4, 'riezen@gmail.com', 'Riezen', 'Dungo', 'password12345', 'F005', 'D001'),
(5, 'dungo_rhovie@gmail.com', 'Rhobie Mae', 'Basalatan', 'passwordpass123', 'F006', 'D004'),
(6, 'enon_john@gmail.com', 'John ', 'Enon', 'password1234567', 'F007', 'D002'),
(7, 'triciarondolo@gmail.com', 'Tricia', 'Rondolo', 'passtricia17', 'F008', 'D003'),
(8, 'iris@gmail.com', 'Iris', 'Frondozo', 'passiris1234', 'F009', 'D002'),
(9, 'anna@gmail.com', 'anna', 'delacruz', 'anna1234', 'F010', 'D002'),
(10, 'eve@gmail.com', 'Eve', 'Torres', 'eve123456', 'F011', 'D001'),
(11, 'santos_riva@gmail.com', 'Riva', 'Santos', 'rivasantos1234', 'F012', 'D001');

-- --------------------------------------------------------

--
-- Table structure for table `internship`
--

CREATE TABLE `internship` (
  `internship_id` varchar(10) NOT NULL,
  `student_id` varchar(10) NOT NULL,
  `company_id` varchar(10) DEFAULT NULL,
  `contact_id` varchar(10) DEFAULT NULL,
  `start_date` date DEFAULT NULL,
  `end_date` date DEFAULT NULL,
  `status` enum('Ongoing','Completed','Pending') NOT NULL DEFAULT 'Pending'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `internship`
--

INSERT INTO `internship` (`internship_id`, `student_id`, `company_id`, `contact_id`, `start_date`, `end_date`, `status`) VALUES
('I001', 'S001', 'C001', 'CC001', '2025-08-25', '2025-12-18', 'Ongoing'),
('I002', 'S002', 'C002', 'CC002', '2025-07-14', '2025-12-15', 'Completed'),
('I003', 'S003', 'C003', 'CC003', '2025-06-28', '2025-12-16', 'Ongoing'),
('I004', 'S004', 'C004', 'CC004', '2025-10-09', '2025-12-17', 'Ongoing'),
('I005', 'S005', NULL, NULL, NULL, NULL, 'Pending'),
('I006', 'S006', NULL, NULL, NULL, NULL, 'Pending');

-- --------------------------------------------------------

--
-- Table structure for table `internship_evaluation`
--

CREATE TABLE `internship_evaluation` (
  `evaluation_id` varchar(10) NOT NULL,
  `internship_id` varchar(10) NOT NULL,
  `grade` decimal(5,2) NOT NULL CHECK (`grade` >= 0.00 and `grade` <= 100.00),
  `evaluation_report` text NOT NULL,
  `evaluation_status` enum('Passed','Failed','Incomplete') NOT NULL,
  `faculty_id` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `internship_evaluation`
--

INSERT INTO `internship_evaluation` (`evaluation_id`, `internship_id`, `grade`, `evaluation_report`, `evaluation_status`, `faculty_id`) VALUES
('E001', 'I001', 99.00, 'Good Participation', 'Passed', 'F001'),
('E002', 'I002', 98.00, 'Perfect Attendance', 'Passed', 'F002'),
('E003', 'I003', 97.00, 'Good in workplace', 'Incomplete', 'F003'),
('E004', 'I004', 98.00, 'Good Communication Skills', 'Passed', 'F004');

-- --------------------------------------------------------

--
-- Table structure for table `section`
--

CREATE TABLE `section` (
  `section_name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `section`
--

INSERT INTO `section` (`section_name`) VALUES
('AB Psychology IV - Wechsler'),
('AB Psychology IV - Wundt'),
('AB Psychology IV-Wechsler'),
('AB Psychology IV-Wundt'),
('BEEd - 4A'),
('BEEd - 4B'),
('BEEd-4A'),
('BEEd-4B'),
('BSA - 4A'),
('BSA-4A'),
('BSBA - 4A'),
('BSBA - 4B'),
('BSBA-4A'),
('BSBA-4B'),
('BSCS - 4A'),
('BSCS-4A'),
('BSECE - 4A'),
('BSECE - 4B'),
('BSECE-4A'),
('BSECE-4B'),
('BSEd - 4 MATH'),
('BSEd - 4A ENGLISH'),
('BSEd - 4A FILIPINO'),
('BSEd - 4B ENGLISH'),
('BSEd - 4B FILIPINO'),
('BSEd-4 MATH'),
('BSEd-4A ENGLISH'),
('BSEd-4A FILIPINO'),
('BSEd-4B ENGLISH'),
('BSEd-4B FILIPINO'),
('BSENT - 4A'),
('BSENT-4A'),
('BSHM - 4A'),
('BSHM - 4B'),
('BSHM - 4C'),
('BSHM - 4D'),
('BSHM - 4E'),
('BSHM-4A'),
('BSHM-4B'),
('BSHM-4C'),
('BSHM-4D'),
('BSHM-4E'),
('BSIT - 4A'),
('BSIT - 4B'),
('BSIT - 4C\n'),
('BSIT - 4D'),
('BSIT-4A'),
('BSIT-4B'),
('BSIT-4C\r\n'),
('BSIT-4D'),
('BSN IV - Abdellah'),
('BSN IV - Nightingale'),
('BSN IV-Abdellah'),
('BSN IV-Nightingale');

-- --------------------------------------------------------

--
-- Table structure for table `student`
--

CREATE TABLE `student` (
  `student_id` varchar(10) NOT NULL,
  `first_name` varchar(120) NOT NULL,
  `last_name` varchar(120) NOT NULL,
  `gender` enum('Male','Female','Other') NOT NULL,
  `section_name` varchar(50) NOT NULL,
  `contact_no` varchar(11) NOT NULL,
  `email` varchar(50) NOT NULL,
  `department_id` varchar(10) NOT NULL,
  `course_id` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `student`
--

INSERT INTO `student` (`student_id`, `first_name`, `last_name`, `gender`, `section_name`, `contact_no`, `email`, `department_id`, `course_id`) VALUES
('S001', 'Tricia Ann', 'Cruz', 'Female', 'BSIT - 4D', '09123456789', 'TC@plppasig.edu', 'D001', 'CR001'),
('S002', 'Francine', 'Dela Rosa', 'Female', 'BSIT - 4A', '09987654321', 'FD@plppasig.edu', 'D001', 'CR001'),
('S003', 'Riezen Lyle', 'Du?go', 'Male', 'BSECE - 4A', '09827394784', 'RD@plpasig.edu', 'D002', 'CR002'),
('S004', 'Juilian Iris', 'Frondozo', 'Female', 'BSN IV - Nightingale', '09345632789', 'JF@plppasig.edu', 'D003', 'CR003'),
('S005', 'Leo', 'Gomez', 'Male', 'BSIT - 4B', '09263728192', 'leo@plpasig.edu.ph', 'D001', 'CR001'),
('S006', 'Lia ', 'Santos', 'Female', 'BSCS - 4A', '09243726387', 'santos_lia@plpasig.edu.ph', 'D001', 'CR004');

-- --------------------------------------------------------

--
-- Stand-in structure for view `vall_internship`
-- (See below for the actual view)
--
CREATE TABLE `vall_internship` (
`Internship ID` varchar(10)
,`First Name` varchar(120)
,`Last Name` varchar(120)
,`Company Name` varchar(120)
,`Supervisor Last Name` varchar(120)
,`Supervisor Contact` varchar(11)
,`Start_Date` date
,`End_Date` date
,`Status` enum('Ongoing','Completed','Pending')
);

-- --------------------------------------------------------

--
-- Stand-in structure for view `vfaculty_table`
-- (See below for the actual view)
--
CREATE TABLE `vfaculty_table` (
`Faculty ID` varchar(10)
,`First Name` varchar(120)
,`Last Name` varchar(120)
,`position` varchar(50)
,`Department` varchar(120)
,`Faculty Contact` varchar(11)
);

-- --------------------------------------------------------

--
-- Stand-in structure for view `vinternship_record`
-- (See below for the actual view)
--
CREATE TABLE `vinternship_record` (
`Internship ID` varchar(10)
,`First Name` varchar(120)
,`Last Name` varchar(120)
,`Company Name` varchar(120)
,`Supervisor Last Name` varchar(120)
,`Supervisor Contact Number` varchar(11)
,`Start-Date` date
,`End-Date` date
,`Status` enum('Ongoing','Completed','Pending')
);

-- --------------------------------------------------------

--
-- Table structure for table `visitlog`
--

CREATE TABLE `visitlog` (
  `visit_id` varchar(10) NOT NULL,
  `internship_id` varchar(10) NOT NULL,
  `faculty_id` varchar(10) NOT NULL,
  `visit_date` date NOT NULL,
  `remarks` text DEFAULT NULL,
  `score` decimal(5,2) DEFAULT NULL CHECK (`score` >= 0.00 and `score` <= 100.00)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `visitlog`
--

INSERT INTO `visitlog` (`visit_id`, `internship_id`, `faculty_id`, `visit_date`, `remarks`, `score`) VALUES
('V001', 'I001', 'F001', '2025-09-15', '', 92.00),
('V002', 'I002', 'F002', '2025-09-26', NULL, 89.00),
('V003', 'I003', 'F003', '2025-09-22', 'Great Performance, Good Job', 95.00),
('V004', 'I004', 'F004', '2025-09-23', NULL, 87.00),
('V005', 'I002', 'F002', '2025-12-03', 'Good Job!', 87.00);

-- --------------------------------------------------------

--
-- Structure for view `vall_internship`
--
DROP TABLE IF EXISTS `vall_internship`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vall_internship`  AS SELECT `i`.`internship_id` AS `Internship ID`, `s`.`first_name` AS `First Name`, `s`.`last_name` AS `Last Name`, `c`.`company_name` AS `Company Name`, `cc`.`contact_last_name` AS `Supervisor Last Name`, `cc`.`contact_contact_no` AS `Supervisor Contact`, `i`.`start_date` AS `Start_Date`, `i`.`end_date` AS `End_Date`, `i`.`status` AS `Status` FROM (((`internship` `i` left join `student` `s` on(`i`.`student_id` = `s`.`student_id`)) left join `company` `c` on(`i`.`company_id` = `c`.`company_id`)) left join `company_contact` `cc` on(`i`.`contact_id` = `cc`.`contact_id`)) ;

-- --------------------------------------------------------

--
-- Structure for view `vfaculty_table`
--
DROP TABLE IF EXISTS `vfaculty_table`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vfaculty_table`  AS SELECT `f`.`faculty_id` AS `Faculty ID`, `f`.`faculty_first_name` AS `First Name`, `f`.`faculty_last_name` AS `Last Name`, `f`.`faculty_position` AS `position`, `d`.`department_name` AS `Department`, `f`.`faculty_contact_no` AS `Faculty Contact` FROM (`faculty` `f` join `department` `d` on(`f`.`department_id` = `d`.`department_id`)) ;

-- --------------------------------------------------------

--
-- Structure for view `vinternship_record`
--
DROP TABLE IF EXISTS `vinternship_record`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `vinternship_record`  AS SELECT `i`.`internship_id` AS `Internship ID`, `s`.`first_name` AS `First Name`, `s`.`last_name` AS `Last Name`, `c`.`company_name` AS `Company Name`, `cc`.`contact_last_name` AS `Supervisor Last Name`, `cc`.`contact_contact_no` AS `Supervisor Contact Number`, `i`.`start_date` AS `Start-Date`, `i`.`end_date` AS `End-Date`, `i`.`status` AS `Status` FROM (((`internship` `i` join `student` `s` on(`i`.`student_id` = `s`.`student_id`)) join `company` `c` on(`i`.`company_id` = `c`.`company_id`)) join `company_contact` `cc` on(`i`.`contact_id` = `cc`.`contact_id`)) ;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `analytics`
--
ALTER TABLE `analytics`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `company`
--
ALTER TABLE `company`
  ADD PRIMARY KEY (`company_id`),
  ADD UNIQUE KEY `company_contact_no` (`company_contact_no`),
  ADD UNIQUE KEY `company_email` (`company_email`);

--
-- Indexes for table `company_contact`
--
ALTER TABLE `company_contact`
  ADD PRIMARY KEY (`contact_id`),
  ADD UNIQUE KEY `contact_contact_no` (`contact_contact_no`),
  ADD UNIQUE KEY `email` (`email`),
  ADD KEY `company_id` (`company_id`),
  ADD KEY `fk_company_contact_student` (`student_id`);

--
-- Indexes for table `course`
--
ALTER TABLE `course`
  ADD PRIMARY KEY (`course_id`),
  ADD KEY `department_id` (`department_id`);

--
-- Indexes for table `department`
--
ALTER TABLE `department`
  ADD PRIMARY KEY (`department_id`);

--
-- Indexes for table `faculty`
--
ALTER TABLE `faculty`
  ADD PRIMARY KEY (`faculty_id`),
  ADD KEY `department_id` (`department_id`),
  ADD KEY `faculty_contact_no` (`faculty_contact_no`) USING BTREE;

--
-- Indexes for table `faculty_section`
--
ALTER TABLE `faculty_section`
  ADD UNIQUE KEY `section_name` (`section_name`),
  ADD KEY `faculty_id` (`faculty_id`);

--
-- Indexes for table `faculty_user_account`
--
ALTER TABLE `faculty_user_account`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD KEY `faculty_id` (`faculty_id`),
  ADD KEY `department_id` (`department_id`);

--
-- Indexes for table `internship`
--
ALTER TABLE `internship`
  ADD PRIMARY KEY (`internship_id`),
  ADD KEY `student_id` (`student_id`),
  ADD KEY `company_id` (`company_id`),
  ADD KEY `contact_id` (`contact_id`);

--
-- Indexes for table `internship_evaluation`
--
ALTER TABLE `internship_evaluation`
  ADD PRIMARY KEY (`evaluation_id`),
  ADD KEY `internship_id` (`internship_id`),
  ADD KEY `faculty_id` (`faculty_id`);

--
-- Indexes for table `section`
--
ALTER TABLE `section`
  ADD PRIMARY KEY (`section_name`);

--
-- Indexes for table `student`
--
ALTER TABLE `student`
  ADD PRIMARY KEY (`student_id`),
  ADD UNIQUE KEY `contact_no` (`contact_no`),
  ADD UNIQUE KEY `email` (`email`),
  ADD KEY `department_id` (`department_id`),
  ADD KEY `course_id` (`course_id`),
  ADD KEY `section_name` (`section_name`);

--
-- Indexes for table `visitlog`
--
ALTER TABLE `visitlog`
  ADD PRIMARY KEY (`visit_id`),
  ADD KEY `internship_id` (`internship_id`),
  ADD KEY `faculty_id` (`faculty_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `analytics`
--
ALTER TABLE `analytics`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `faculty_user_account`
--
ALTER TABLE `faculty_user_account`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `company_contact`
--
ALTER TABLE `company_contact`
  ADD CONSTRAINT `company_contact_ibfk_1` FOREIGN KEY (`company_id`) REFERENCES `company` (`company_id`) ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_company_contact_student` FOREIGN KEY (`student_id`) REFERENCES `student` (`student_id`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Constraints for table `course`
--
ALTER TABLE `course`
  ADD CONSTRAINT `course_ibfk_1` FOREIGN KEY (`department_id`) REFERENCES `department` (`department_id`) ON UPDATE CASCADE;

--
-- Constraints for table `faculty`
--
ALTER TABLE `faculty`
  ADD CONSTRAINT `faculty_ibfk_1` FOREIGN KEY (`department_id`) REFERENCES `department` (`department_id`) ON UPDATE CASCADE;

--
-- Constraints for table `faculty_section`
--
ALTER TABLE `faculty_section`
  ADD CONSTRAINT `faculty_section_ibfk_1` FOREIGN KEY (`faculty_id`) REFERENCES `faculty` (`faculty_id`),
  ADD CONSTRAINT `faculty_section_ibfk_2` FOREIGN KEY (`section_name`) REFERENCES `section` (`section_name`);

--
-- Constraints for table `faculty_user_account`
--
ALTER TABLE `faculty_user_account`
  ADD CONSTRAINT `faculty_user_account_ibfk_1` FOREIGN KEY (`faculty_id`) REFERENCES `faculty` (`faculty_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `faculty_user_account_ibfk_2` FOREIGN KEY (`department_id`) REFERENCES `department` (`department_id`) ON UPDATE CASCADE;

--
-- Constraints for table `internship`
--
ALTER TABLE `internship`
  ADD CONSTRAINT `internship_ibfk_1` FOREIGN KEY (`student_id`) REFERENCES `student` (`student_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `internship_ibfk_2` FOREIGN KEY (`company_id`) REFERENCES `company` (`company_id`),
  ADD CONSTRAINT `internship_ibfk_3` FOREIGN KEY (`contact_id`) REFERENCES `company_contact` (`contact_id`);

--
-- Constraints for table `internship_evaluation`
--
ALTER TABLE `internship_evaluation`
  ADD CONSTRAINT `internship_evaluation_ibfk_1` FOREIGN KEY (`internship_id`) REFERENCES `internship` (`internship_id`) ON UPDATE CASCADE,
  ADD CONSTRAINT `internship_evaluation_ibfk_2` FOREIGN KEY (`faculty_id`) REFERENCES `faculty` (`faculty_id`) ON UPDATE CASCADE;

--
-- Constraints for table `student`
--
ALTER TABLE `student`
  ADD CONSTRAINT `student_ibfk_1` FOREIGN KEY (`department_id`) REFERENCES `department` (`department_id`) ON UPDATE CASCADE,
  ADD CONSTRAINT `student_ibfk_2` FOREIGN KEY (`course_id`) REFERENCES `course` (`course_id`) ON UPDATE CASCADE,
  ADD CONSTRAINT `student_ibfk_3` FOREIGN KEY (`section_name`) REFERENCES `section` (`section_name`) ON UPDATE CASCADE;

--
-- Constraints for table `visitlog`
--
ALTER TABLE `visitlog`
  ADD CONSTRAINT `visitlog_ibfk_1` FOREIGN KEY (`internship_id`) REFERENCES `internship` (`internship_id`) ON UPDATE CASCADE,
  ADD CONSTRAINT `visitlog_ibfk_2` FOREIGN KEY (`faculty_id`) REFERENCES `faculty` (`faculty_id`) ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
