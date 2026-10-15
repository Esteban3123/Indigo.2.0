CREATE TABLE [Glosas].[GlosasPublicParametersInterface] (
    [Id]                              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParametersInterfaceId]           INT          NULL,
    [YearAccount]                     INT          NOT NULL,
    [DebitAccount]                    VARCHAR (15) NOT NULL,
    [AcceptanceObjectionsConcept]     VARCHAR (3)  NULL,
    [AcceptanceObjectionsConceptPast] VARCHAR (3)  NULL,
    [AcceptancReiterationsConcept]    VARCHAR (3)  NULL,
    [TimeStamp]                       ROWVERSION   NOT NULL,
    CONSTRAINT [PK_GlosasPublicParametersInterface] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosasPublicParametersInterface_GlosasParametersInterface] FOREIGN KEY ([ParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosasPublicParametersInterface] NOCHECK CONSTRAINT [FK_GlosasPublicParametersInterface_GlosasParametersInterface];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de aceptacion de reiteraciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptancReiterationsConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de Aceptacion de objeciones para vigencias Anteriores', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptanceObjectionsConceptPast';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de Aceptacion de objeciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptanceObjectionsConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Debito', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'DebitAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año de las cuentas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'YearAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo configuracion de interface', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'ParametersInterfaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasPublicParametersInterface', @level2type = N'COLUMN', @level2name = N'Id';

