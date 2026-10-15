CREATE TABLE [Payroll].[BranchOffice] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [CompanyId]        INT          NULL,
    [Name]             VARCHAR (50) NOT NULL,
    [Address]          VARCHAR (80) NOT NULL,
    [Telephone]        VARCHAR (15) NOT NULL,
    [CityId]           INT          NOT NULL,
    [PayrollManagerId] INT          CONSTRAINT [DF_BranchOffice_PayrollManagerId] DEFAULT ((11094)) NULL,
    [State]            BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_BranchOffice] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BranchOffice_City] FOREIGN KEY ([CityId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_BranchOffice_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Payroll].[Company] ([Id]),
    CONSTRAINT [FK_BranchOffice_Employee] FOREIGN KEY ([PayrollManagerId]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [Payroll].[BranchOffice] NOCHECK CONSTRAINT [FK_BranchOffice_City];


GO
ALTER TABLE [Payroll].[BranchOffice] NOCHECK CONSTRAINT [FK_BranchOffice_Company];


GO
ALTER TABLE [Payroll].[BranchOffice] NOCHECK CONSTRAINT [FK_BranchOffice_Employee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BranchOffice', @level2type = N'COLUMN', @level2name = N'TimeStamp';

