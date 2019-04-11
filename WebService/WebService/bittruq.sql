-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le :  jeu. 11 avr. 2019 à 18:42
-- Version du serveur :  5.7.23
-- Version de PHP :  7.0.32

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `bittruq`
--

-- --------------------------------------------------------

--
-- Structure de la table `transaction`
--

DROP TABLE IF EXISTS `transaction`;
CREATE TABLE IF NOT EXISTS `transaction` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `FromWallet` varchar(255) NOT NULL,
  `ToWallet` varchar(255) NOT NULL,
  `Content` double NOT NULL,
  `Datetime` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `transaction`
--

INSERT INTO `transaction` (`Id`, `FromWallet`, `ToWallet`, `Content`, `Datetime`) VALUES
(1, 'drs', 'fsef', 12, ''),
(2, '2ea7ece4b775d18d3a760ea6660d59b526882a36b3e9c62d84e60ea93c1176d1', '2ea7ece4b775d18d3a760ea6660d59b526882a36b3e9c62d84e60ea93c1176d2', 45, ''),
(3, 'b8261069360de03e0cb09adec3cfca8221df7bb9eae22c69c1d8140fdfea80a1', 'c9952a6be5330136ce183fef1a267d557e71f3018e21dd56b0d7f818c477a29d', 20, '2019-01-01 00:00:00');

-- --------------------------------------------------------

--
-- Structure de la table `wallets`
--

DROP TABLE IF EXISTS `wallets`;
CREATE TABLE IF NOT EXISTS `wallets` (
  `Hash` varchar(255) NOT NULL,
  `Value` decimal(10,0) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `wallets`
--

INSERT INTO `wallets` (`Hash`, `Value`) VALUES
('2ea7ece4b775d18d3a760ea6660d59b526882a36b3e9c62d84e60ea93c1176d1', '1000'),
('e2392e38fff48c055e3e255af82ab9b09dde5f4851619dac84c322edc2864778', '1000'),
('3dc92f046af6f8f5fdca7d976ff75b458c7ca7d099496cb74e315f942257fe54', '1000'),
('af664e49ded1b06df185654648c5b00d165ea8b95737063bb0530ec4e29c7221', '1000'),
('e4f0e5e8545205cdf2ba6185e3414799f695b562719ed601f6c0ffce8248947c', '1000'),
('75231cbadedffac495b4ee03b0ca71b546e301f43fea2686a37b1c7267ee4c3b', '1000'),
('f477ad488a54e807e5d9f1c6b9906977df5f3fec3f524979518c8f5a26213b71', '1000'),
('eade3e094b9b14d70f3515239af3398dfc45b131702251d0f53ef2cebeda795f', '1000'),
('b5474c53fed460db6ad3c2453b1ebd62f3900d19eaf075e393d328e5e64bc1f3', '1000'),
('ca18de4403c4e76e221ac6fc3d0aacf69b83f6ced1551db7412b5ca8d9677cb1', '1000');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
