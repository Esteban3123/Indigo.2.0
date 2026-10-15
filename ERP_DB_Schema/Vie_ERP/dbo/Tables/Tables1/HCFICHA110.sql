CREATE TABLE [dbo].[HCFICHA110] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [RNTIPDOC]            INT           NULL,
    [RNIPCODPACI]         CHAR (15)     NULL,
    [RNFECHNACI]          DATE          NULL,
    [RNEDAD]              INT           NULL,
    [RNSEXO]              INT           NULL,
    [RNPESO]              INT           NULL,
    [RNTALLA]             INT           NULL,
    [GESTPARTO]           INT           NULL,
    [CLASIFPESO]          INT           NULL,
    [SITIOPARTO]          INT           NULL,
    [MULTIPLIEMBARA]      INT           NULL,
    [EMBARAZOPREVIOS]     INT           NULL,
    [NUMHIJOS]            INT           NULL,
    [NIVELEDUCATIVO]      INT           NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_FICHA110] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA110_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA110_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA110] NOCHECK CONSTRAINT [CK_HCFICHA110_JSON];


GO
ALTER TABLE [dbo].[HCFICHA110] NOCHECK CONSTRAINT [FK_HCFICHA110_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel educativo de la madre :   1 = Primaria   2 = Secundaria   3 = Técnico o superior   4 = Ninguna   5 = No sabe o sin Información --> ( item eliminado desde version V01_2020-03-06 )   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'NIVELEDUCATIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de los hijos vivos contando el actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'NUMHIJOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de embarazos previos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'EMBARAZOPREVIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Multiplicidad del embarazo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'MULTIPLIEMBARA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sitio de atención del parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'SITIOPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación peso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'CLASIFPESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Semanas gestación al parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'GESTPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla al nacer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNTALLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso al nacer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNPESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo RN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNSEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad RN (días)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recien nacido = RN  Fecha nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNFECHNACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recien nacido   Codigo del paciente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNIPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Documento del Recien Nacido -->    1 = RC      2 = MS    3 = PE    4 = CN  --> ( item nuevo desde version V01_2020-03-06 )  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'RNTIPDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de Ficha notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA110', @level2type = N'COLUMN', @level2name = N'ID';

