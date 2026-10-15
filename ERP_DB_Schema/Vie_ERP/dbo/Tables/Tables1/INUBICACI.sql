CREATE TABLE [dbo].[INUBICACI] (
    [AUUBICACI]       CHAR (20)    NOT NULL,
    [DEPMUNCOD]       CHAR (5)     NOT NULL,
    [UBICODIGO]       CHAR (6)     NULL,
    [UBINOMBRE]       CHAR (100)   NOT NULL,
    [TIPOUBICA]       INT          NOT NULL,
    [INDAUDFOR]       NUMERIC (18) NOT NULL,
    [ESTADO]          INT          NULL,
    [IDCOMUNA]        INT          NULL,
    [ID]              INT          IDENTITY (1, 1) NOT NULL,
    [UbicationId]     INT          NULL,
    [UbicationTypeId] INT          NULL,
    CONSTRAINT [PK_INUbicaci] PRIMARY KEY CLUSTERED ([AUUBICACI] ASC),
    CONSTRAINT [FK_INUBICACI_ADCOMUNAS] FOREIGN KEY ([IDCOMUNA]) REFERENCES [dbo].[ADCOMUNAS] ([Id]),
    CONSTRAINT [FK_INUbicaci_INMunicip] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD]) ON UPDATE CASCADE
);


GO
ALTER TABLE [dbo].[INUBICACI] NOCHECK CONSTRAINT [FK_INUBICACI_ADCOMUNAS];


GO
ALTER TABLE [dbo].[INUBICACI] NOCHECK CONSTRAINT [FK_INUbicaci_INMunicip];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id tipo de ubicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'UbicationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la ubicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'UbicationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla ADCOMUNAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'IDCOMUNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Ubicacion 1->Acitvo 2->Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Columna para Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Ubicacion:  0 = Urbano  1 = Rural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'TIPOUBICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Ubicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'UBINOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Ubicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'UBICODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Departamento y municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Ubicacion y municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUBICACI', @level2type = N'COLUMN', @level2name = N'AUUBICACI';

