CREATE TABLE [dbo].[ODOPARTRA] (
    [CONSECTRA]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGOTRA]            NCHAR (10)      NOT NULL,
    [DESCRITRA]            NCHAR (150)     NOT NULL,
    [CODSERIPS]            CHAR (20)       NULL,
    [INDICECPO]            CHAR (1)        NULL,
    [INDICECEO]            CHAR (1)        NULL,
    [IMAGENTRA]            VARBINARY (MAX) NULL,
    [APLICATRA]            CHAR (1)        NULL,
    [COLORSTRA]            NCHAR (40)      NULL,
    [UBICACION]            CHAR (1)        NOT NULL,
    [OBSERVTRA]            NVARCHAR (200)  NULL,
    [ESTADOTRA]            BIT             NOT NULL,
    [APLICAOTROSTRATAMIEN] BIT             NULL,
    CONSTRAINT [PK_ODOPARTRA] PRIMARY KEY CLUSTERED ([CONSECTRA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda aplica otros tratamientos  1 = true', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'APLICAOTROSTRATAMIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del tratamiento    True: Activo  False: Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'ESTADOTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicacion:  0: No Aplica  1: Diente  2: Sextante  3: Cuadrante  4: Maxilar   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'UBICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Para determinar si aplica a  1: Diente  2: Superficie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'APLICATRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indice CEO    N: No Aplica  C: Cariado  E: Extraido  O: Obturado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'INDICECEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indice CPO    N: No Aplica  C: Cariado  P: Perdido  O: Obturado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'INDICECPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del servicio IPS    Campo que queda obsoleto desde el día 08-03-2019 ya que se crea otra tabla para alamacenar los CUPS llamada ODOPARTRACUPSD.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';

