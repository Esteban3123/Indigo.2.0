param([Parameter(Mandatory)][ValidatePattern('^CodexBillingOwnerConfirmation_[A-Za-z0-9_]+$')][string]$TestDatabase)
$ErrorActionPreference = 'Stop'
$connectionString = "Server=(localdb)\MSSQLLocalDB;Database=$TestDatabase;Integrated Security=true;TrustServerCertificate=true"
$first = [System.Data.SqlClient.SqlConnection]::new($connectionString)
$second = [System.Data.SqlClient.SqlConnection]::new($connectionString)
$transaction = $null
try {
    $first.Open()
    $second.Open()
    $ownerId = Get-Random -Minimum 100000 -Maximum 999999
    $setup = $first.CreateCommand()
    $setup.CommandText = @'
SET QUOTED_IDENTIFIER ON; SET ANSI_NULLS ON; SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON; SET CONCAT_NULL_YIELDS_NULL ON; SET ARITHABORT ON;
INSERT dbo.ADCONCOEX VALUES (@Id, '0', 1, NULL, 'synthetic', 'RACE');
INSERT dbo.HCHISPACA VALUES (@Id, 'synthetic', 'RACE', 'clinician', '8');
'@
    [void]$setup.Parameters.AddWithValue('@Id', $ownerId)
    [void]$setup.ExecuteNonQuery()
    $options = $second.CreateCommand()
    $options.CommandText = 'SET QUOTED_IDENTIFIER ON; SET ANSI_NULLS ON; SET ANSI_PADDING ON; SET ANSI_WARNINGS ON; SET CONCAT_NULL_YIELDS_NULL ON; SET ARITHABORT ON;'
    [void]$options.ExecuteNonQuery()

    # Clinical completion owns the row until its transaction commits.
    $transaction = $first.BeginTransaction()
    $clinical = $first.CreateCommand()
    $clinical.Transaction = $transaction
    $clinical.CommandText = 'SELECT NUMCONCIT FROM dbo.ADCONCOEX WITH (UPDLOCK,HOLDLOCK) WHERE CODCONCEC=@Id; UPDATE dbo.ADCONCOEX SET CONESTADO=3,IDHCHISPACA=@Id WHERE CODCONCEC=@Id;'
    [void]$clinical.Parameters.AddWithValue('@Id', $ownerId)
    [void]$clinical.ExecuteNonQuery()
    $callback = $second.CreateCommand()
    $callback.CommandTimeout = 15
    $callback.CommandText = "EXEC Clinical.ConfirmBillingAppointment @Id,@Id,'synthetic','RACE','test',@Message,'race-test';"
    [void]$callback.Parameters.AddWithValue('@Id', $ownerId)
    [void]$callback.Parameters.AddWithValue('@Message', "race-$ownerId")
    $pending = $callback.ExecuteNonQueryAsync()
    Start-Sleep -Milliseconds 400
    if ($pending.IsCompleted) { throw 'Confirmation did not wait for the clinical transaction.' }
    $transaction.Commit()
    $transaction = $null
    [void]$pending.GetAwaiter().GetResult()
    $verify = $second.CreateCommand()
    $verify.CommandText = 'SELECT COUNT(*) FROM Clinical.OutboxEvent WHERE JSON_VALUE(PayloadJson,''$.data.appointmentId'')=CONVERT(varchar(20),@Id);'
    [void]$verify.Parameters.AddWithValue('@Id', $ownerId)
    if ([int]$verify.ExecuteScalar() -ne 1) { throw 'Expected exactly one completion event after the race.' }

    # The reverse order exposes the definitive ID to the clinical writer.
    $transaction = $first.BeginTransaction()
    $confirm = $first.CreateCommand()
    $confirm.Transaction = $transaction
    $confirm.CommandText = $callback.CommandText
    [void]$confirm.Parameters.AddWithValue('@Id', $ownerId)
    [void]$confirm.Parameters.AddWithValue('@Message', "race-$ownerId")
    [void]$confirm.ExecuteNonQuery()
    $read = $second.CreateCommand()
    $read.CommandTimeout = 15
    $read.CommandText = 'SELECT TRY_CONVERT(int,NUMCONCIT) FROM dbo.ADCONCOEX WITH(UPDLOCK,HOLDLOCK) WHERE CODCONCEC=@Id;'
    [void]$read.Parameters.AddWithValue('@Id', $ownerId)
    $waiting = $read.ExecuteScalarAsync()
    Start-Sleep -Milliseconds 400
    if ($waiting.IsCompleted) { throw 'Clinical read did not wait for the confirmation transaction.' }
    $transaction.Commit()
    $transaction = $null
    if ([int]$waiting.GetAwaiter().GetResult() -ne $ownerId) { throw 'Clinical read did not see the definitive ID.' }
    Write-Output 'PASS: both lock orderings serialize confirmation and clinical completion.'
}
finally {
    if ($null -ne $transaction) { $transaction.Rollback(); $transaction.Dispose() }
    $first.Dispose()
    $second.Dispose()
}
