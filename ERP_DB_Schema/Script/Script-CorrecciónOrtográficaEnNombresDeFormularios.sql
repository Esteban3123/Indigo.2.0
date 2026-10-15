begin tran t123
begin try

    UPDATE Security.Form
    SET Name = 'Rangos de Provisión'
    WHERE Id = 681;

    UPDATE Security.Form
    SET Name = 'Líneas de Distribución'
    WHERE Id = 584;

    UPDATE Security.Form
    SET Name = 'Conceptos de Conciliación'
    WHERE Id = 2211;

COMMIT TRAN t123;
end TRY
begin catch 
  IF @@TRANCOUNT > 0   
       ROLLBACK TRAN t123;
end catch  