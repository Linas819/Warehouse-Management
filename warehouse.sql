-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jul 04, 2025 at 02:13 PM
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
-- Database: `warehouse`
--

-- --------------------------------------------------------

--
-- Table structure for table `product`
--

CREATE TABLE `product` (
  `ProductId` varchar(20) NOT NULL,
  `Name` varchar(20) NOT NULL,
  `Ean` varchar(20) NOT NULL,
  `Type` varchar(20) NOT NULL,
  `Weight` float NOT NULL,
  `Price` float NOT NULL,
  `Quantity` int(11) NOT NULL,
  `CreatedBy` varchar(20) NOT NULL,
  `CreatedDateTime` datetime NOT NULL,
  `UpdatedBy` varchar(20) NOT NULL,
  `UpdatedDateTime` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `product`
--

INSERT INTO `product` (`ProductId`, `Name`, `Ean`, `Type`, `Weight`, `Price`, `Quantity`, `CreatedBy`, `CreatedDateTime`, `UpdatedBy`, `UpdatedDateTime`) VALUES
('Apl002', 'Apples', '4301', 'Fruit', 2.14, 3.15, 6, 'admin', '2025-07-04 15:10:03', 'admin', '2025-07-04 15:11:57'),
('Ban001', 'Banana', '4011', 'Fruit', 1.12, 1.21, 5, 'admin', '2025-07-04 15:09:26', 'admin', '2025-07-04 15:12:01');

-- --------------------------------------------------------

--
-- Table structure for table `product_price_history`
--

CREATE TABLE `product_price_history` (
  `Id` int(11) NOT NULL,
  `ProductId` varchar(20) NOT NULL,
  `Price` float NOT NULL,
  `CreatedBy` varchar(20) NOT NULL,
  `CreatedDateTime` datetime(6) NOT NULL DEFAULT current_timestamp(6)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `product_price_history`
--

INSERT INTO `product_price_history` (`Id`, `ProductId`, `Price`, `CreatedBy`, `CreatedDateTime`) VALUES
(4, 'Ban001', 1.01, 'admin', '2025-07-04 15:09:26.852941'),
(5, 'Apl002', 3.12, 'admin', '2025-07-04 15:10:03.076359'),
(6, 'Apl002', 3.15, 'admin', '2025-07-04 15:11:57.262055'),
(7, 'Ban001', 1.21, 'admin', '2025-07-04 15:12:01.346784');

-- --------------------------------------------------------

--
-- Table structure for table `product_quantity_history`
--

CREATE TABLE `product_quantity_history` (
  `Id` int(11) NOT NULL,
  `ProductId` varchar(20) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `CreatedBy` varchar(20) NOT NULL,
  `CreatedDateTime` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `product_quantity_history`
--

INSERT INTO `product_quantity_history` (`Id`, `ProductId`, `Quantity`, `CreatedBy`, `CreatedDateTime`) VALUES
(40, 'Ban001', 6, 'admin', '2025-07-04 15:09:26.800815'),
(41, 'Apl002', 8, 'admin', '2025-07-04 15:10:03.068885'),
(42, 'Apl002', 6, 'admin', '2025-07-04 15:11:30.733999'),
(43, 'Ban001', 5, 'admin', '2025-07-04 15:11:37.642283');

-- --------------------------------------------------------

--
-- Stand-in structure for view `users_view`
-- (See below for the actual view)
--
CREATE TABLE `users_view` (
`UserId` varchar(20)
,`Username` varchar(20)
,`Password` varchar(50)
,`FirstName` varchar(20)
,`LastName` varchar(20)
,`CreatedDateTime` datetime(6)
);

-- --------------------------------------------------------

--
-- Structure for view `users_view`
--
DROP TABLE IF EXISTS `users_view`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `users_view`  AS SELECT `users`.`users`.`UserId` AS `UserId`, `users`.`users`.`Username` AS `Username`, `users`.`users`.`Password` AS `Password`, `users`.`users`.`FirstName` AS `FirstName`, `users`.`users`.`LastName` AS `LastName`, `users`.`users`.`CreatedDateTime` AS `CreatedDateTime` FROM `users`.`users` ;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `product`
--
ALTER TABLE `product`
  ADD PRIMARY KEY (`ProductId`),
  ADD KEY `ProductCreated` (`CreatedBy`),
  ADD KEY `ProductUpdated` (`UpdatedBy`);

--
-- Indexes for table `product_price_history`
--
ALTER TABLE `product_price_history`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `PriceProductId` (`ProductId`),
  ADD KEY `ProductPriceCreated` (`CreatedBy`);

--
-- Indexes for table `product_quantity_history`
--
ALTER TABLE `product_quantity_history`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `ProductQuantityCreated` (`CreatedBy`),
  ADD KEY `QauntityProduct` (`ProductId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `product_price_history`
--
ALTER TABLE `product_price_history`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `product_quantity_history`
--
ALTER TABLE `product_quantity_history`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=44;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `product`
--
ALTER TABLE `product`
  ADD CONSTRAINT `ProductCreated` FOREIGN KEY (`CreatedBy`) REFERENCES `users`.`users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `ProductUpdated` FOREIGN KEY (`UpdatedBy`) REFERENCES `users`.`users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `product_price_history`
--
ALTER TABLE `product_price_history`
  ADD CONSTRAINT `PriceProductId` FOREIGN KEY (`ProductId`) REFERENCES `product` (`ProductId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `ProductPriceCreated` FOREIGN KEY (`CreatedBy`) REFERENCES `users`.`users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `product_quantity_history`
--
ALTER TABLE `product_quantity_history`
  ADD CONSTRAINT `ProductQuantityCreated` FOREIGN KEY (`CreatedBy`) REFERENCES `users`.`users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `QauntityProduct` FOREIGN KEY (`ProductId`) REFERENCES `product` (`ProductId`) ON DELETE CASCADE ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
