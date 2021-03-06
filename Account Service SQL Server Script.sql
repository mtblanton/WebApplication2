USE [master]
GO
/****** Object:  Database [Account_Service]    Script Date: 8/12/2016 4:25:44 AM ******/
CREATE DATABASE [Account_Service]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Account_Service', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL12.USABLE_PROJECT\MSSQL\DATA\Account_Service.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Account_Service_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL12.USABLE_PROJECT\MSSQL\DATA\Account_Service_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Account_Service] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Account_Service].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Account_Service] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Account_Service] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Account_Service] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Account_Service] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Account_Service] SET ARITHABORT OFF 
GO
ALTER DATABASE [Account_Service] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Account_Service] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Account_Service] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Account_Service] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Account_Service] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Account_Service] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Account_Service] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Account_Service] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Account_Service] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Account_Service] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Account_Service] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Account_Service] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Account_Service] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Account_Service] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Account_Service] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Account_Service] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Account_Service] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Account_Service] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Account_Service] SET  MULTI_USER 
GO
ALTER DATABASE [Account_Service] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Account_Service] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Account_Service] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Account_Service] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Account_Service] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Account_Service]
GO
/****** Object:  Table [dbo].[ActivationPending]    Script Date: 8/12/2016 4:25:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ActivationPending](
	[ActivationCode] [char](36) NOT NULL CONSTRAINT [DF_ActivationPending_ActivationCode]  DEFAULT (newid()),
	[GroupNumber] [varchar](50) NOT NULL,
	[TaxId] [char](10) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[EmailAddress] [varchar](200) NOT NULL,
	[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_ActivationPending_CreateDate]  DEFAULT (getutcdate()),
	[ChiefAdmin] [bit] NOT NULL,
	[GroupName] [varchar](200) NOT NULL,
 CONSTRAINT [PK_ActivationPending] PRIMARY KEY CLUSTERED 
(
	[ActivationCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Errors]    Script Date: 8/12/2016 4:25:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Errors](
	[Application] [varchar](200) NOT NULL,
	[ErrDescription] [varchar](200) NOT NULL,
	[ErrDate] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserAccounts]    Script Date: 8/12/2016 4:25:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserAccounts](
	[UserId] [uniqueidentifier] NOT NULL CONSTRAINT [DF_UserAccounts_UserId]  DEFAULT (newid()),
	[GroupNumber] [varchar](50) NOT NULL,
	[FirstName] [nchar](10) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[EmailAddress] [varchar](50) NOT NULL,
	[ChiefAdmin] [bit] NOT NULL,
	[IdActive] [bit] NOT NULL,
	[ActiveDate] [datetime] NOT NULL,
	[ActivationCode] [char](36) NOT NULL,
	[InactiveDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserAccounts] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store pending activation information for accounts' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ActivationPending'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store information about application and service errors' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Errors'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' Store active and inactive user account information for users' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserAccounts'
GO
USE [master]
GO
ALTER DATABASE [Account_Service] SET  READ_WRITE 
GO
