CREATE TABLE [dbo].[PRMODELOHC] (
    [ID]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]              VARCHAR (5)     NOT NULL,
    [NOMBRE]              VARCHAR (100)   NOT NULL,
    [DESCRIPCION]         VARCHAR (500)   NULL,
    [ESTADO]              BIT             NOT NULL,
    [FECHACREA]           DATETIME        NOT NULL,
    [USUACREA]            CHAR (20)       NOT NULL,
    [IMAGEN]              VARBINARY (MAX) NOT NULL,
    [MOSTRARCURVAS]       BIT             CONSTRAINT [DF_PRMODELOHC_MOSTRARCURVAS] DEFAULT ((0)) NOT NULL,
    [MOSTRARVACUNACION]   BIT             NULL,
    [MOSTRARTFG]          BIT             CONSTRAINT [DF_PRMODELOHC_MOSTRARTFG] DEFAULT ((0)) NOT NULL,
    [MOSTRARJUNTAMEDICA]  BIT             CONSTRAINT [DF__PRMODELOH__MOSTR__3188B38A] DEFAULT ((0)) NOT NULL,
    [MOSTRARFAMILIOGRAMA] BIT             NULL,
    [MOSTRARECOMAPA]      BIT             NULL,
    [SpecialtyType]       INT             NULL,
    [Gender]              INT             NULL,
    CONSTRAINT [PK_PRMODELOHC] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1 Femenino   2 Masculino    3 Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'Gender';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0 - Ninguno   1 Salud visual   2 Nutrición   3 Psicología    4 Anestesiología   5 Mastología   6 Oncología   7 Hematología   8 Ortopedia   9 Cirugía plástica   10 Urología   11 Dermatología   12 Cuidados paliativos   13 Fisioterapia   14 Radioterapia   15 Cirugía   16 Especialidades clínicas   17 Ginecología   18 Radiología   19 Trabajo social ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'SpecialtyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'mostrar ecomapa 1=  true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARECOMAPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda mostrar  familiograma   1 = Si   0 = No ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARFAMILIOGRAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si se debe mostrar el control de junta medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARJUNTAMEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'mostrar TFG  1 = True    0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARTFG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda mostrar vacunacion  1 = si   0 = No ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARVACUNACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda mostrar curvas de crecimiento  1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'MOSTRARCURVAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la imagen de modelo HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'IMAGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Usuario de  Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'USUACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el estado   1 = Activo    0 =Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Modelo HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Modelo HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Modelo HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRMODELOHC', @level2type = N'COLUMN', @level2name = N'ID';

