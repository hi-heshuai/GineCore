/*
 Navicat Premium Data Transfer

 Source Server         : 118.25.83.200
 Source Server Type    : MariaDB
 Source Server Version : 100407
 Source Host           : 118.25.83.200:3306
 Source Schema         : mydb

 Target Server Type    : MariaDB
 Target Server Version : 100407
 File Encoding         : 65001

 Date: 28/08/2019 16:16:35
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for log
-- ----------------------------
DROP TABLE IF EXISTS `log`;
CREATE TABLE `log`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `OperateName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Result` tinyint(1) DEFAULT NULL,
  `OperateUserId` int(11) DEFAULT NULL,
  `OperateUserName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `OperateTime` datetime(0) DEFAULT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedAt` datetime(0) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedAt` datetime(0) DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for menus
-- ----------------------------
DROP TABLE IF EXISTS `menus`;
CREATE TABLE `menus`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Icon` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `LinkUrl` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Type` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Sort` int(11) DEFAULT NULL,
  `ParentId` int(11) DEFAULT NULL,
  `EnableMarked` tinyint(1) DEFAULT NULL,
  `Key` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedAt` datetime(0) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedAt` datetime(0) DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of menus
-- ----------------------------
INSERT INTO `menus` VALUES (1, 'person', NULL, NULL, '用户管理', 1, NULL, 1, 'system', NULL, NULL, NULL, NULL);
INSERT INTO `menus` VALUES (2, 'person', NULL, NULL, '用户管理', 1, 1, 1, 'user', NULL, NULL, NULL, NULL);
INSERT INTO `menus` VALUES (3, 'ios-people', NULL, NULL, '用户组管理', 2, 1, 1, 'role', NULL, NULL, NULL, NULL);
INSERT INTO `menus` VALUES (4, 'android-menu', NULL, NULL, '菜单管理', 3, 1, 1, 'menu', NULL, NULL, NULL, NULL);

-- ----------------------------
-- Table structure for plan
-- ----------------------------
DROP TABLE IF EXISTS `plan`;
CREATE TABLE `plan`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `CompletionTime` datetime(0) DEFAULT NULL,
  `Recollections` longtext CHARACTER SET utf8 COLLATE utf8_general_ci,
  `Status` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedAt` datetime(0) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedAt` datetime(0) DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for rolemenu
-- ----------------------------
DROP TABLE IF EXISTS `rolemenu`;
CREATE TABLE `rolemenu`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RolesId` int(11) DEFAULT NULL,
  `MenusId` int(11) DEFAULT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedAt` datetime(0) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedAt` datetime(0) DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of rolemenu
-- ----------------------------
INSERT INTO `rolemenu` VALUES (1, 1, 1, NULL, '0001-01-01 12:00:00', NULL, '0001-01-01 12:00:00');
INSERT INTO `rolemenu` VALUES (2, 1, 2, NULL, '0001-01-01 12:00:00', NULL, '0001-01-01 12:00:00');
INSERT INTO `rolemenu` VALUES (3, 1, 3, NULL, '0001-01-01 12:00:00', NULL, '0001-01-01 12:00:00');
INSERT INTO `rolemenu` VALUES (4, 1, 4, NULL, '0001-01-01 12:00:00', NULL, '0001-01-01 12:00:00');

-- ----------------------------
-- Table structure for roles
-- ----------------------------
DROP TABLE IF EXISTS `roles`;
CREATE TABLE `roles`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Describe` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedAt` datetime(0) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedAt` datetime(0) DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of roles
-- ----------------------------
INSERT INTO `roles` VALUES (1, '管理员', '管理员', 1681, '2019-08-28 03:48:45', NULL, '0001-01-01 12:00:00');

-- ----------------------------
-- Table structure for userinfo
-- ----------------------------
DROP TABLE IF EXISTS `userinfo`;
CREATE TABLE `userinfo`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `RealName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Password` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `UserType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `LoginIpAddress` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `LoginToken` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `LoginTime` datetime(0) DEFAULT NULL,
  `Avatar` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Birthday` datetime(0) DEFAULT NULL,
  `MobilePhone` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Email` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `WeChat` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `IsRoot` tinyint(1) DEFAULT NULL,
  `EnableMarked` tinyint(1) DEFAULT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedAt` datetime(0) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedAt` datetime(0) DEFAULT NULL,
  PRIMARY KEY (`Id`, `UserName`, `RealName`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of userinfo
-- ----------------------------
INSERT INTO `userinfo` VALUES (1, 'heshuai', '贺帅', 'E55FE09286599CE5', NULL, NULL, 'c467d7f8-840a-41a6-add3-168165a0e2f6', '2019-08-28 03:47:50', NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL);
INSERT INTO `userinfo` VALUES (2, 'admin', '管理员', 'C4FCFA956BF7E2B8', NULL, NULL, '257f89b3-7e62-4a03-91ba-2c96bb404a3c', '2019-08-28 04:08:34', 'http://localhost:9090/Upload/24f6f82d713445fcb7b235896b89aa86b3.png', '2019-05-15 12:00:00', NULL, NULL, NULL, 0, 1, 1343, '2019-08-28 03:48:32', NULL, '0001-01-01 12:00:00');

-- ----------------------------
-- Table structure for userrole
-- ----------------------------
DROP TABLE IF EXISTS `userrole`;
CREATE TABLE `userrole`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RolesId` int(11) DEFAULT NULL,
  `UserInfoId` int(11) DEFAULT NULL,
  `CreatedBy` int(11) DEFAULT NULL,
  `CreatedAt` datetime(0) DEFAULT NULL,
  `UpdatedBy` int(11) DEFAULT NULL,
  `UpdatedAt` datetime(0) DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of userrole
-- ----------------------------
INSERT INTO `userrole` VALUES (1, 1, 2, NULL, '0001-01-01 12:00:00', NULL, '0001-01-01 12:00:00');

SET FOREIGN_KEY_CHECKS = 1;
