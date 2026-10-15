CREATE TABLE [dbo].[HCFICRENFOL] (
    [ID]           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMEFOLIO]    NCHAR (10)                                                                       NOT NULL,
    [FECINGUREN]   DATETIME                                                                         NULL,
    [FECREGISTRO]  DATETIME                                                                         NOT NULL,
    [ERC]          TINYINT                                                                          NULL,
    [ETILOGIA]     TINYINT                                                                          NULL,
    [ESTADIO]      TINYINT                                                                          NULL,
    [FECDIAGNO]    DATETIME                                                                         NULL,
    [PROGATERC]    TINYINT                                                                          NULL,
    [PROGATERCDES] VARCHAR (50)                                                                     NULL,
    [RECIBETRR]    TINYINT                                                                          NULL,
    [RECIBETRRDES] VARCHAR (50)                                                                     NULL,
    [TRRHEMODIA]   TINYINT                                                                          NULL,
    [TRRDIAPERI]   TINYINT                                                                          NULL,
    [TRRTERNODIA]  TINYINT                                                                          NULL,
    [MODINITRR]    TINYINT                                                                          NULL,
    [FECHINITRR]   DATETIME                                                                         NULL,
    [TFGINITRR]    FLOAT (53)                                                                       NULL,
    [RECTRAREN]    TINYINT                                                                          NULL,
    [IPSREATRA]    VARCHAR (50)                                                                     NULL,
    [INDITRANS]    TINYINT                                                                          NULL,
    [FECINGLIES]   DATETIME                                                                         NULL,
    [IPSLISESPE]   VARCHAR (50)                                                                     NULL,
    [HTA]          TINYINT                                                                          NULL,
    [FECDIAGHTA]   DATETIME                                                                         NULL,
    [RECIBEIECA]   TINYINT                                                                          NULL,
    [RECIBEARA]    TINYINT                                                                          NULL,
    [DM]           TINYINT                                                                          NULL,
    [FECDIAGDM]    DATETIME                                                                         NULL,
    [HEPATIB]      TINYINT                                                                          NULL,
    [FECDIAGHB]    DATETIME                                                                         NULL,
    [HEPATIC]      TINYINT                                                                          NULL,
    [FECDIAGHC]    DATETIME                                                                         NULL,
    [VACUHEPA]     TINYINT                                                                          NULL,
    [ITCANACT]     TINYINT                                                                          NULL,
    [ITINFCRO]     TINYINT                                                                          NULL,
    [ITNOTRANS]    TINYINT                                                                          NULL,
    [ITESPVIDA]    TINYINT                                                                          NULL,
    [ITPOTILIM]    TINYINT                                                                          NULL,
    [ITENFCARD]    TINYINT                                                                          NULL,
    [ITINFEVIH]    TINYINT                                                                          NULL,
    [ITINFEVHC]    TINYINT                                                                          NULL,
    [ITENFINMU]    TINYINT                                                                          NULL,
    [ITENFPULM]    TINYINT                                                                          NULL,
    [ITOTRENFC]    TINYINT                                                                          NULL,
    [PERITOINFE]   NUMERIC (18)                                                                     NULL,
    [FECHAINSERC]  DATETIME                                                                         NULL,
    [MOTIVOTRR]    VARCHAR (500)                                                                    NULL,
    CONSTRAINT [PK_HCFICRENFOL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICRENFOL_HCFICRENFOL] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCFICRENFOL] NOCHECK CONSTRAINT [FK_HCFICRENFOL_HCFICRENFOL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de TRR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'MOTIVOTRR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que me va a guardar la fecha de la inserción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECHAINSERC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peritonitis Infecciosa:  98 = No Aplica (El usuario no ha estado en diálisis periotoneal en ningún momento en los últimos 12 meses)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'PERITOINFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicación de transplante: Otras enfermedades crónicas (11) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITOTRENFC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicación de transplante: Enfermedad pulmonar crónica (10) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITENFPULM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicación de transplante: Enfermedad inmunológica activa (9) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITENFINMU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicación de transplante: Infección por el VHC (8) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITINFEVHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicación de transplante: Infección por el VIH (7) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITINFEVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicación de transplante: Enfermedad cardiaca, cerebrovascular o vascular periférica (6) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITENFCARD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicación de transplante: Potenciales limitaciones al autocuidado y adherencia al tratamiento post trasplante (5) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITPOTILIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicación de transplante: Esperanza de vida menor o igual a 6 meses (4) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITESPVIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicación de transplante: NO ha manifestado su deseo de trasplantarse (3) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITNOTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicación de transplante: Infección crónica o activa no tratada o no controlada (2) 1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITINFCRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicación de transplante: Cancer Activo (1)   1:Si 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ITCANACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vacuna Hepatitis:  98=No Aplica  1= Si, esquema completo  2= Si, esquema incompleto   3= No  99= Sin dato.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'VACUHEPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de diagnostico Hepatitos C:  1845-01-01= No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hepatitis C:  i. 1= Si  ii. 2= No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'HEPATIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de diagnostico Hepatitis B:  1845-01-01= No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGHB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hepatitis B:  i. 1= Si  ii. 2= No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'HEPATIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de diagnostico DM:  1845-01-01= No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGDM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diabetes Mellitus:  1 = Si  2 = No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'DM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recibe ARA II:  98 =No Aplica  1. 1=Si  2. 2=No, No fue formulado dentro del plan terapéutico  3. 3=No, Aunque fue formulado dentro del plan terapéutico  4. 99=Sin Dato  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECIBEARA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recibe IECA:  98 =No Aplica  1. 1=Si  2. 2=No, No fue formulado dentro del plan terapéutico  3. 3=No, Aunque fue formulado dentro del plan terapéutico  4. 99=Sin Dato  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECIBEIECA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de diagnostico HTA:  1845-01-01= No Aplic  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGHTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hipertención arterial:  i. 1 = Si  ii. 2 = No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'HTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IPS Lista de espera:  98= No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'IPSLISESPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de ingreso a lista de espera:  1845-01-01= No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECINGLIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicación de transplante:  i. 1= No Contraindicado  ii. 2= Contraindicado: Las siguientes opciones son de múltiple selección  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'INDITRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IPS realizó transplante:  98=No Aplica  99= Sin Dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'IPSREATRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recibio transplante renal:  i. 1=Si y está Funcional: Inhabilitar si en “Recibe TRR”  seleccionó una opción diferente a “2”  ii. 3=Si y no está funcional  iii. 5=No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECTRAREN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TFG al iniciar la terapia de remplazo renal de forma cronica por primer vez:  98=No Aplica  99= Sin Dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'TFGINITRR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de inicio de terapia de remplazo renal:  1845-01-01 = No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECHINITRR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modo de inicio primera terapia de remplazo renal:  i. 1= Urgencia  ii. 2= Programada  iii. 3= Desconocido  iv. 4= Primera TRR diferente a Diálisis  98=No Aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'MODINITRR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recibe terapia de remplazo renal: Si, Terapia No Dialítica  98=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'TRRTERNODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recibe terapia de remplazo renal: Si, Diálisis Peritoneal  1. 1= Manual  2. 2= Automatizada  98=No Aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'TRRDIAPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recibe terapia de remplazo renal: Si, Hemodiálisis  1. 1= Fístula  2. 2= Catéter  98=No Aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'TRRHEMODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recibe terapia de remplazo renal,  se guarda la descripcion correspondiente  la selección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECIBETRRDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recibe terapia de remplazo renal,  se guarda el codigo correspondiente  la selección:  98=No Aplica  i. 1= Si, HemodiálisisH   ii. 11= Si, Diálisis Peritoneal   iii. 111= Si, Terapia No Dialítica  iv. 2= No ha recibido por primera vez TRR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'RECIBETRR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Programa de atención de ERC, se guarda la descripción correspondiente a la selección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'PROGATERCDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Programa de atención de ERC, se guarda el codigo correspondiente  la selección:  98=No Aplica  i. 1= Si, Nefroprotección  ii. 11= Si, Prediálisis  iii. 2= No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'PROGATERC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de diagnositco:  1845-01-01= No Aplica  1800-01-01 = Desconocida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estadío:  98= No Tiene ERC  99= Desconocido  1  2  3  4  5  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ESTADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Etilogía:  98= No Tiene ERC  i. 1 = HTA o DM  ii. 2 = Autoinmune  iii. 3 = Nefropatía Obstructiva  iv. 4 = Enfermedad Poliquística  v. 5 = otras  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ETILOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Enfermedad renal cronica:  i. 0 = No.   ii. 1 = Si  iii. 2 = Indeterminado entre estadios 1, 2 o sin ERC  iv. 3 = El usuario no ha sido estudiado para ERC    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ERC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Registro De La Ficha Renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de ingreso a unidad Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'FECINGUREN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero folio que nos indica que la ficha renal ya se le hizo una Historia de Ingreso, se registra solo en las unidades Funcionales de Tipo Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Códgio del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICRENFOL', @level2type = N'COLUMN', @level2name = N'ID';

