CREATE TABLE [dbo].[HCFICHA591] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [NOMAPE]              VARCHAR (50)  NULL,
    [TIPOID]              VARCHAR (50)  NULL,
    [NUMIDEN]             VARCHAR (50)  NULL,
    [EDAD]                VARCHAR (50)  NULL,
    [NUMHIJVIVOS]         VARCHAR (50)  NULL,
    [NUMHIJMUERT]         VARCHAR (50)  NULL,
    [ESTADCONYU]          INT           NULL,
    [ULTIMOANIO]          VARCHAR (50)  NULL,
    [NECROPSIA]           BIT           NULL,
    [CAUSAA]              VARCHAR (MAX) NULL,
    [CAUSAB]              VARCHAR (MAX) NULL,
    [CAUSAC]              VARCHAR (MAX) NULL,
    [CAUSAD]              VARCHAR (MAX) NULL,
    [OTROESTAD]           VARCHAR (MAX) NULL,
    [CAUSAPROB]           VARCHAR (MAX) NULL,
    [CLASIFIN]            INT           NULL,
    [HISTCLINICA]         BIT           NULL,
    [PRUEBLAB]            BIT           NULL,
    [INTERROG]            BIT           NULL,
    [ASISMED]             BIT           NULL,
    [SITIODEF]            INT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA591] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA591_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA591_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA591] NOCHECK CONSTRAINT [CK_HCFICHA591_JSON];


GO
ALTER TABLE [dbo].[HCFICHA591] NOCHECK CONSTRAINT [FK_HCFICHA591_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el sitio de defunción (1. Hospital / clínica, 2. Centro / puesto de salud, 3. Casa/domicilio, 4. Lugar de trabajo, 5. Vía pública, 6. Otro sitio, 9. Sin información)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'SITIODEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la asistencia médica (checked or unchecked, causa de muerte determinada por)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'ASISMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el Interrogatorio a familiares o testigos (checkout or uncheckout, causas de defunción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'INTERROG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la prueba de laboratorio (checkout or uncheckout, causas de defunción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'PRUEBLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la historia clinica (checkout or uncheckout, causas de defunción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'HISTCLINICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la clasificación final (1. muerte por dnt, 2. muerte por ira, 3. muerte por edad)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CLASIFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa probable de muerte sin certificación médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAPROB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda estado otro estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'OTROESTAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la causa directa D (causas de defunción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la causa directa C (causas de defunción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la causa directa B (causas de defunción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la causa directa A (causas de defunción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la necropsia (checkout, or uncheckout, causas de defunción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NECROPSIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ultimo año aprobado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'ULTIMOANIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el estado conyugal (1. No esta casada y lleva dos o mas años viviendo con pareja, 2. No esta casada y lleva menos de dos años viviendo con pareja, 3. Divorciada, 4. Viuda)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'ESTADCONYU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el numero de hijos muertos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NUMHIJMUERT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el número de hijos vivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NUMHIJVIVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la edad de la madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'EDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el número de identificación (datos de la madre del menor)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NUMIDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo ID (Documento Identificación) de la Madre --> desde versión ''V01_2020-03-06'' es con una enumeración:  1= RC - Registro Civil   2= TI - Tarjeta de Identidad   3= CC - Cédula de Ciudadanía   4= CE - Cédula de Extranjería   5= PA - Pasaporte   6= MS - Menor Sin Identificación   7= AS - Adulto Sin Identificación   8= PE - Permiso Especial de Permanencia   9= CN - Certificado de Nacido Vivo   10= CD - Carnet Diplomático   11= SC - Salvoconducto   13= DE - Documento Extranjero    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'TIPOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el nombre y apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NOMAPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'ID';

