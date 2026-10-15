CREATE TABLE [dbo].[PRHCPAGE] (
    [ID]         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC] INT NOT NULL,
    [CODPAGE]    INT NOT NULL,
    [ORDEN]      INT NOT NULL,
    CONSTRAINT [PK_PRHCPAGE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCPAGE_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCPAGE] NOCHECK CONSTRAINT [FK_PRHCPAGE_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Orden que lleva el page dentro del modelo de HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE', @level2type = N'COLUMN', @level2name = N'ORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del PAGE de la HC - CONTROLES ESTANDAR   
1. MC / EA  
2. ANTECEDENTES  
3. REVISION POR SISTEMAS
4. EXAMEN FISICO  
5. VALORACION DE PARACLINICOS
6. ANALISIS  
7. IMPRESION DIAGNOSTICA
8. PLAN DE MANEJO  
9. DESTINO DEL PACIENTE 
10. ODONTOGRAMA 
11.PERIODONTOGRAMA
12.AIEPI  
13.INFORMACION MEDICA GENERAL
14.ESCALAS  
15.VALORACION PARACLINICOS HISTORICO 
16.OTROS  
17.VALORACION CLINICA DE OPTOMETRIA
18.Valoración materno perinatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE', @level2type = N'COLUMN', @level2name = N'CODPAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con el modelo de HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPAGE', @level2type = N'COLUMN', @level2name = N'ID';

