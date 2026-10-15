CREATE TABLE [dbo].[HCFICHA560] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [GESTACION]           INT           NULL,
    [PARTOSVAG]           INT           NULL,
    [CESAREAS]            INT           NULL,
    [ABORTOS]             INT           NULL,
    [MUERTOS]             INT           NULL,
    [VIVOS]               INT           NULL,
    [NUMCPN]              INT           NULL,
    [SEMANACPN]           INT           NULL,
    [TIPOPARTO]           INT           NULL,
    [FECHAPARTO]          DATETIME      NULL,
    [SITIODEFUN]          INT           NULL,
    [PARTOATENDI]         INT           NULL,
    [SITIOPARTO]          INT           NULL,
    [NIVELATEN2]          INT           NULL,
    [MOMENTOMUER]         INT           NULL,
    [EDADGESTACIO]        INT           NULL,
    [EDADNEONATAL]        INT           NULL,
    [PESONACER]           INT           NULL,
    [TALLANACER]          INT           NULL,
    [SEXO]                INT           NULL,
    [CAUSAMUERTE]         INT           NULL,
    [CAUFETALES]          CHAR (4)      NULL,
    [CAUNEONATALES]       CHAR (4)      NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA560] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA560_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA560_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA560] NOCHECK CONSTRAINT [CK_HCFICHA560_JSON];


GO
ALTER TABLE [dbo].[HCFICHA560] NOCHECK CONSTRAINT [FK_HCFICHA560_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa neonatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CAUNEONATALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa fetales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CAUFETALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa de muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CAUSAMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genero (M. masculino, F. Femenino, I. Indeterminado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla al nacer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'TALLANACER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso al nacer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'PESONACER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad neonatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'EDADNEONATAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'EDADGESTACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Momento en que ocurrio la muerte respecto al parto (1. anteparto, 2. intraparto, 3. prealta en postpart, 5. postalta en postparto, 6. reingreso en postparto, 7. no aplica. nunca fue a la institucion de salud en postparto)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'MOMENTOMUER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de atención (1. baja complejidad, 2. mediana complejidad, 3. alta complejidad)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'NIVELATEN2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio del parto (1. institucional, 2. domicilio, 3. otro)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'SITIOPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parto atendido por (1. médico general, 2. médico obstetra, 3. enfermera, 4. aux. enfermeria, 5. promotor, 6. partera, 7. otro, 8. ella misma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'PARTOATENDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio de defunción (6. domicilio, 8. baja complejidad, 9. mediana complejidad, 10. alta complejidad, 11. UCI, 12. traslado interinstitucional, 13. traslado a domicilio IPS, 7. otro)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'SITIODEFUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'FECHAPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo del parto (1. vaginal, 2. cesárea, 3. instrumentado, 4. ignorado, 5. no nacio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semana inicio CPN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'SEMANACPN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número CPN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'NUMCPN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de vivos (antecedentes prenatales)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'VIVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de muertos (antecedentes prenatales)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'MUERTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de abortos (antecedentes prenatales)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'ABORTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cesareas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CESAREAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de partos vaginales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'PARTOSVAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gestación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'GESTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'ID';

