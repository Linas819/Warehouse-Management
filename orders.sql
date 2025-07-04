-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jul 04, 2025 at 02:12 PM
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
-- Database: `orders`
--

-- --------------------------------------------------------

--
-- Table structure for table `address`
--

CREATE TABLE `address` (
  `AddressId` varchar(20) NOT NULL,
  `Country` varchar(20) NOT NULL,
  `Zip` varchar(20) NOT NULL,
  `Region` varchar(20) NOT NULL,
  `City` varchar(20) NOT NULL,
  `Street` varchar(20) NOT NULL,
  `House` varchar(20) NOT NULL,
  `Apartment` varchar(20) DEFAULT NULL,
  `CreatedBy` varchar(20) NOT NULL,
  `CreatedDateTime` datetime(6) NOT NULL,
  `UpdatedBy` varchar(20) NOT NULL,
  `UpdatedDateTime` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `address`
--

INSERT INTO `address` (`AddressId`, `Country`, `Zip`, `Region`, `City`, `Street`, `House`, `Apartment`, `CreatedBy`, `CreatedDateTime`, `UpdatedBy`, `UpdatedDateTime`) VALUES
('L5KKB92-1801', 'Lithuania', '54211', 'Kaunas', 'Kaunas', 'Basanavičiaus', '9', '24', 'admin', '2025-07-04 15:10:48.525979', 'admin', '2025-07-04 15:10:48.526019'),
('L5VVV91419', 'Lithuania', '53112', 'Vilnius', 'Vilnius', 'Verkių', '9', '12', 'admin', '2025-07-04 15:11:10.741853', 'admin', '2025-07-04 15:11:10.741853');

-- --------------------------------------------------------

--
-- Table structure for table `order`
--

CREATE TABLE `order` (
  `OrderId` varchar(20) NOT NULL,
  `AddressFrom` varchar(20) NOT NULL,
  `AddressTo` varchar(20) NOT NULL,
  `CreatedBy` varchar(20) NOT NULL,
  `CreatedDateTime` datetime(6) NOT NULL,
  `UpdatedBy` varchar(20) NOT NULL,
  `UpdatedDateTime` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `order`
--

INSERT INTO `order` (`OrderId`, `AddressFrom`, `AddressTo`, `CreatedBy`, `CreatedDateTime`, `UpdatedBy`, `UpdatedDateTime`) VALUES
('ord1', 'L5KKB92-1801', 'L5VVV91419', 'admin', '2025-07-04 15:11:23.480145', 'admin', '2025-07-04 15:11:23.480172');

-- --------------------------------------------------------

--
-- Table structure for table `orderlines`
--

CREATE TABLE `orderlines` (
  `OrderLineId` int(11) NOT NULL,
  `OrderId` varchar(20) NOT NULL,
  `ProductId` varchar(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `CreatedBy` varchar(20) NOT NULL,
  `CreatedDateTime` datetime(6) NOT NULL DEFAULT current_timestamp(6)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `orderlines`
--

INSERT INTO `orderlines` (`OrderLineId`, `OrderId`, `ProductId`, `Quantity`, `CreatedBy`, `CreatedDateTime`) VALUES
(27, 'ord1', 'Apl002', 2, 'admin', '2025-07-04 15:11:30.726754'),
(28, 'ord1', 'Ban001', 1, 'admin', '2025-07-04 15:11:37.642228');

-- --------------------------------------------------------

--
-- Stand-in structure for view `product_view`
-- (See below for the actual view)
--
CREATE TABLE `product_view` (
`ProductId` varchar(20)
,`Name` varchar(20)
,`Ean` varchar(20)
,`Type` varchar(20)
,`Weight` float
,`Price` float
,`Quantity` int(11)
,`CreatedBy` varchar(20)
,`CreatedDateTime` datetime
,`UpdatedBy` varchar(20)
,`UpdatedDateTime` datetime
);

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
-- Structure for view `product_view`
--
DROP TABLE IF EXISTS `product_view`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `product_view`  AS SELECT `warehouse`.`product`.`ProductId` AS `ProductId`, `warehouse`.`product`.`Name` AS `Name`, `warehouse`.`product`.`Ean` AS `Ean`, `warehouse`.`product`.`Type` AS `Type`, `warehouse`.`product`.`Weight` AS `Weight`, `warehouse`.`product`.`Price` AS `Price`, `warehouse`.`product`.`Quantity` AS `Quantity`, `warehouse`.`product`.`CreatedBy` AS `CreatedBy`, `warehouse`.`product`.`CreatedDateTime` AS `CreatedDateTime`, `warehouse`.`product`.`UpdatedBy` AS `UpdatedBy`, `warehouse`.`product`.`UpdatedDateTime` AS `UpdatedDateTime` FROM `warehouse`.`product` ;

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
-- Indexes for table `address`
--
ALTER TABLE `address`
  ADD PRIMARY KEY (`AddressId`),
  ADD KEY `UserCreated` (`CreatedBy`),
  ADD KEY `UserUpdated` (`UpdatedBy`);

--
-- Indexes for table `order`
--
ALTER TABLE `order`
  ADD PRIMARY KEY (`OrderId`),
  ADD KEY `AddressFrom` (`AddressFrom`),
  ADD KEY `OrderCreatedUser` (`CreatedBy`),
  ADD KEY `OrderUpdatedUser` (`UpdatedBy`),
  ADD KEY `AddressTo` (`AddressTo`);

--
-- Indexes for table `orderlines`
--
ALTER TABLE `orderlines`
  ADD PRIMARY KEY (`OrderLineId`),
  ADD KEY `OrderLineCreated` (`CreatedBy`),
  ADD KEY `OrderId` (`OrderId`),
  ADD KEY `ProductId` (`ProductId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `orderlines`
--
ALTER TABLE `orderlines`
  MODIFY `OrderLineId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `address`
--
ALTER TABLE `address`
  ADD CONSTRAINT `UserCreated` FOREIGN KEY (`CreatedBy`) REFERENCES `users`.`users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `UserUpdated` FOREIGN KEY (`UpdatedBy`) REFERENCES `users`.`users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `order`
--
ALTER TABLE `order`
  ADD CONSTRAINT `AddressFrom` FOREIGN KEY (`AddressFrom`) REFERENCES `address` (`AddressId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `AddressTo` FOREIGN KEY (`AddressTo`) REFERENCES `address` (`AddressId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `OrderCreatedUser` FOREIGN KEY (`CreatedBy`) REFERENCES `users`.`users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `OrderUpdatedUser` FOREIGN KEY (`UpdatedBy`) REFERENCES `users`.`users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `orderlines`
--
ALTER TABLE `orderlines`
  ADD CONSTRAINT `OrderId` FOREIGN KEY (`OrderId`) REFERENCES `order` (`OrderId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `OrderLineCreated` FOREIGN KEY (`CreatedBy`) REFERENCES `users`.`users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `ProductId` FOREIGN KEY (`ProductId`) REFERENCES `warehouse`.`product` (`ProductId`) ON DELETE CASCADE ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
