CREATE TABLE [Contract].[Groupers] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)    NOT NULL,
    [Description]          VARCHAR (100)   NOT NULL,
    [ParentId]             INT             NULL,
    [UserNumber]           INT             CONSTRAINT [DF_Groupers_UserNumber] DEFAULT ((0)) NOT NULL,
    [UserMin]              INT             CONSTRAINT [DF_Groupers_UserMin] DEFAULT ((0)) NOT NULL,
    [UserMax]              INT             CONSTRAINT [DF_Groupers_UserMax] DEFAULT ((0)) NOT NULL,
    [ProjectCME]           NUMERIC (18)    CONSTRAINT [DF_Groupers_ProjectCME] DEFAULT ((0)) NOT NULL,
    [Frequence]            NUMERIC (18, 8) CONSTRAINT [DF_Groupers_Frequence] DEFAULT ((0)) NOT NULL,
    [TotalContract]        NUMERIC (18)    CONSTRAINT [DF_Groupers_TotalContract] DEFAULT ((0)) NOT NULL,
    [UserValue]            NUMERIC (18)    CONSTRAINT [DF_Groupers_UserValue] DEFAULT ((0)) NOT NULL,
    [MinimunRange]         INT             CONSTRAINT [DF_Groupers_MinimunRange] DEFAULT ((0)) NOT NULL,
    [MaximunRange]         INT             CONSTRAINT [DF_Groupers_MaximunRange] DEFAULT ((0)) NOT NULL,
    [MeasurementUnit]      TINYINT         CONSTRAINT [DF_Groupers_MeasurementUnit] DEFAULT ((0)) NOT NULL,
    [WarningFor]           INT             CONSTRAINT [DF_Groupers_WarningFor] DEFAULT ((0)) NOT NULL,
    [WarningMessage]       VARCHAR (500)   NULL,
    [MaximunRangeRestrict] BIT             CONSTRAINT [DF_Groupers_MaximunRangeRestrict] DEFAULT ((0)) NOT NULL,
    [RestrictMessage]      VARCHAR (500)   NULL,
    [Status]               BIT             NOT NULL,
    [CreationUser]         VARCHAR (20)    NOT NULL,
    [CreationDate]         DATETIME        NOT NULL,
    [ModificationUser]     VARCHAR (20)    NULL,
    [ModificationDate]     DATETIME        NULL,
    [TimeStamp]            ROWVERSION      NOT NULL,
    [Version]              INT             DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Groupers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Groupers_Groupers] FOREIGN KEY ([ParentId]) REFERENCES [Contract].[Groupers] ([Id])
);


GO
ALTER TABLE [Contract].[Groupers] NOCHECK CONSTRAINT [FK_Groupers_Groupers];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado: 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje de Restricción', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'RestrictMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Restringir Rango Máximo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'MaximunRangeRestrict';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje de Advertencia', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'WarningMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Advertir a partir de', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'WarningFor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rango Máximo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'MaximunRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rango Minimo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'MinimunRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del usuario', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'UserValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contrato total', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'TotalContract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Frequence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Proyecto CME', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'ProjectCME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Maximo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'UserMax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Minimo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'UserMin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de usuario', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'UserNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del padre', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'ParentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Id';

