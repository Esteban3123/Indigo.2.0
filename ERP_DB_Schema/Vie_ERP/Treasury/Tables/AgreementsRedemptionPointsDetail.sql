CREATE TABLE [Treasury].[AgreementsRedemptionPointsDetail] (
    [Id]                           INT             IDENTITY (1, 1) NOT NULL,
    [AgreementsRedemptionPointsId] INT             NOT NULL,
    [PointsAmount]                 INT             NOT NULL,
    [PointsValue]                  DECIMAL (18, 2) NOT NULL,
    [InitialDate]                  DATETIME        NOT NULL,
    [EndDate]                      DATETIME        NOT NULL,
    [CreationUser]                 VARCHAR (50)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    [ModificationUser]             VARCHAR (50)    NULL,
    [ModificationDate]             DATETIME        NULL,
    CONSTRAINT [PK_AgreementsRedemptionPointsDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AgreementsRedemptionPointsDetail_AgreementsRedemptionPoints] FOREIGN KEY ([AgreementsRedemptionPointsId]) REFERENCES [Treasury].[AgreementsRedemptionPoints] ([Id])
);


GO
ALTER TABLE [Treasury].[AgreementsRedemptionPointsDetail] NOCHECK CONSTRAINT [FK_AgreementsRedemptionPointsDetail_AgreementsRedemptionPoints];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la fecha de la última modificación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AgreementsRedemptionPointsDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el usuario de la última modificación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AgreementsRedemptionPointsDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la fecha de creación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AgreementsRedemptionPointsDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el usuario de creación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AgreementsRedemptionPointsDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AgreementsRedemptionPointsDetail', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AgreementsRedemptionPointsDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de los puntos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AgreementsRedemptionPointsDetail', @level2type = N'COLUMN', @level2name = N'PointsValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de puntos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AgreementsRedemptionPointsDetail', @level2type = N'COLUMN', @level2name = N'PointsAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cabecera.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AgreementsRedemptionPointsDetail', @level2type = N'COLUMN', @level2name = N'AgreementsRedemptionPointsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'AgreementsRedemptionPointsDetail', @level2type = N'COLUMN', @level2name = N'Id';

