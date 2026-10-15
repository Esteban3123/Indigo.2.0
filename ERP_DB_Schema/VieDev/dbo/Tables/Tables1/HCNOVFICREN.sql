CREATE TABLE [dbo].[HCNOVFICREN] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHAR]  INT           NOT NULL,
    [MOTNOVEDA] TINYINT       NULL,
    [FECNOVEDA] DATETIME      NULL,
    [CAUMUERTE] TINYINT       NULL,
    [FECMUERTE] DATETIME      NULL,
    [OTROMOT]   VARCHAR (200) NULL,
    [USUREGNOV] CHAR (20)     NOT NULL,
    CONSTRAINT [PK_HCNOVFICREN] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNOVFICREN_HCRENFICH] FOREIGN KEY ([IDFICHAR]) REFERENCES [dbo].[HCRENFICH] ([ID])
);


GO
ALTER TABLE [dbo].[HCNOVFICREN] NOCHECK CONSTRAINT [FK_HCNOVFICREN_HCRENFICH];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Causa de muerte:  1=Enfermedad renal crónica  2= Enfermedad Cardiovascular  3= Cáncer  4= Infección  5= Causa diferente a 1, 2, 3 y 4  6= Causa Externa  99= Sin dato.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'CAUMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo del cambio de estado o novedad:  1= Falleció (Guarda como Novedad (V79))  5= Persona que firmó alta voluntaria del tratamiento prescrito (Guarda como Novedad (V79))  6= Persona que se desafilió (Guarda como Novedad (V79))  7= Persona que abandona la terapia (Guarda como Novedad (V79))  9= Persona que regresa  terapia (Guarda como Novedad (V79))  99= Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOVFICREN', @level2type = N'COLUMN', @level2name = N'MOTNOVEDA';

