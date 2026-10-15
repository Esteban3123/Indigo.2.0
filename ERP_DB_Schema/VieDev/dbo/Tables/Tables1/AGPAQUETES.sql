CREATE TABLE [dbo].[AGPAQUETES] (
    [ID]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]                     VARCHAR (4)   NOT NULL,
    [NOMBRE]                     VARCHAR (100) NOT NULL,
    [ESTADO]                     BIT           NOT NULL,
    [TIPO]                       INT           NULL,
    [ParameterizationCostCenter] BIT           NULL,
    CONSTRAINT [PK_AGPAQUETES] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se guardará el campo de parametrizar por centro de costo, conun valor binario, donde cero significará "NO" y 1 significará "SI"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETES', @level2type = N'COLUMN', @level2name = N'ParameterizationCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo :   1 - Procedimientos quirúrgicos  2 - Procedimientos diagnósticos  3 - Paquetes de enfermería', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETES', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Paquetes Quirurgicos   1 = true= Activo  0=False= Inactivo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETES', @level2type = N'COLUMN', @level2name = N'ESTADO';

