CREATE TABLE [dbo].[HCESCALAASSIST] (
    [ID]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCESCALAID]     INT           NOT NULL,
    [CODIGO]         INT           NOT NULL,
    [RESULTADO]      INT           NOT NULL,
    [INTERPRETACION] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_HCESCALAASSIST] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCALAASSIST_HCESCALAS] FOREIGN KEY ([HCESCALAID]) REFERENCES [dbo].[HCESCALAS] ([ID])
);


GO
ALTER TABLE [dbo].[HCESCALAASSIST] NOCHECK CONSTRAINT [FK_HCESCALAASSIST_HCESCALAS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Interpretacion de acuerdo al puntaje de la sustancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'INTERPRETACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Puntaje para cada sustancia. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '001->Tabaco, 002->Alcohol, 003->Cannabis, 004->Cocaina, 005->Anfetaminas, 006->Inhalantes, 007->Sedantes, 008->Alucinogenos, 009->Opiaceos, 010->Otras Drogas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla cabecera llamada HCESCALAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'HCESCALAID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALAASSIST', @level2type = N'COLUMN', @level2name = N'ID';

