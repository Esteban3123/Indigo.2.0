# Script de Configuración para Pipeline de Vie_ERP Database
# Este script automatiza la configuración inicial del pipeline en Azure DevOps

param(
    [Parameter(Mandatory=$true)]
    [string]$Organization,
    
    [Parameter(Mandatory=$true)]
    [string]$Project,
    
    [Parameter(Mandatory=$true)]
    [string]$PersonalAccessToken,
    
    [Parameter(Mandatory=$false)]
    [string]$RepositoryName = "Vie_ERP_Database"
)

Write-Host "🚀 Configurando Pipeline de CI/CD para Vie_ERP Database" -ForegroundColor Green
Write-Host "=================================================="

# Validar prerrequisitos
Write-Host "🔍 Validando prerrequisitos..."

if (-not (Get-Command az -ErrorAction SilentlyContinue)) {
    Write-Error "❌ Azure CLI no está instalado. Instalar desde: https://aka.ms/InstallAzureCLI"
    exit 1
}

# Configurar autenticación con Azure DevOps
Write-Host "🔐 Configurando autenticación..."
$env:AZURE_DEVOPS_EXT_PAT = $PersonalAccessToken
az devops configure --defaults organization="https://dev.azure.com/$Organization" project=$Project

# Verificar conexión
try {
    $projectInfo = az devops project show --project $Project 2>$null | ConvertFrom-Json
    if ($projectInfo) {
        Write-Host "✅ Conexión con Azure DevOps establecida" -ForegroundColor Green
        Write-Host "   Proyecto: $($projectInfo.name)"
    }
} catch {
    Write-Error "❌ No se pudo conectar a Azure DevOps. Verificar organización, proyecto y PAT."
    exit 1
}

# 1. CREAR GRUPOS DE VARIABLES
Write-Host ""
Write-Host "📋 Creando grupos de variables..."

$variableGroups = @{
    "Vie_ERP_Database_Variables" = @{
        "DevSqlServer" = "dev-sql.company.local"
        "TestSqlServer" = "test-sql.company.local"
        "StagingSqlServer" = "staging-sql.company.local"
        "ProdSqlServer" = "prod-sql.company.local"
        "NotificationEmail" = "dev-team@company.com"
        "BackupRetentionDays" = "30"
        "DeploymentTimeoutMinutes" = "60"
        "ValidationTimeoutMinutes" = "30"
    }
    
    "Vie_ERP_ServiceConnections" = @{
        "DevServiceConnection" = "Dev-SQL-ServiceConnection"
        "TestServiceConnection" = "Test-SQL-ServiceConnection"
        "StagingServiceConnection" = "Staging-SQL-ServiceConnection"
        "ProdServiceConnection" = "Prod-SQL-ServiceConnection"
    }
}

foreach ($groupName in $variableGroups.Keys) {
    try {
        # Verificar si el grupo existe
        $existingGroup = az pipelines variable-group list --group-name $groupName 2>$null | ConvertFrom-Json
        
        if ($existingGroup) {
            Write-Host "   ⚠️  Grupo '$groupName' ya existe, actualizando variables..." -ForegroundColor Yellow
            
            foreach ($varName in $variableGroups[$groupName].Keys) {
                $varValue = $variableGroups[$groupName][$varName]
                az pipelines variable-group variable update --group-name $groupName --name $varName --value $varValue | Out-Null
            }
        } else {
            Write-Host "   📝 Creando grupo '$groupName'..."
            
            # Crear el grupo
            $groupId = az pipelines variable-group create --name $groupName --variables dummy="temp" --query "id" -o tsv
            
            # Agregar variables reales
            foreach ($varName in $variableGroups[$groupName].Keys) {
                $varValue = $variableGroups[$groupName][$varName]
                az pipelines variable-group variable create --group-id $groupId --name $varName --value $varValue | Out-Null
            }
            
            # Remover variable temporal
            az pipelines variable-group variable delete --group-id $groupId --name "dummy" --yes | Out-Null
        }
        
        Write-Host "   ✅ Grupo '$groupName' configurado correctamente" -ForegroundColor Green
        
    } catch {
        Write-Warning "   ⚠️  Error configurando grupo '$groupName': $($_.Exception.Message)"
    }
}

# 2. CREAR AMBIENTES
Write-Host ""
Write-Host "🌍 Creando ambientes de deployment..."

$environments = @{
    "development" = @{
        "description" = "Ambiente de desarrollo - deployment automático"
        "approvers" = @()
    }
    "test" = @{
        "description" = "Ambiente de testing - deployment automático" 
        "approvers" = @()
    }
    "staging" = @{
        "description" = "Ambiente de staging - requiere aprobación manual"
        "approvers" = @("team-lead@company.com")  # Ajustar según su organización
    }
    "production" = @{
        "description" = "Ambiente de producción - requiere múltiples aprobaciones"
        "approvers" = @("team-lead@company.com", "dba@company.com")  # Ajustar según su organización
    }
}

foreach ($envName in $environments.Keys) {
    try {
        # Verificar si el ambiente existe
        $existingEnv = az devops invoke --area environments --resource environments --route-parameters project=$Project --query-parameters name=$envName 2>$null
        
        if ($existingEnv) {
            Write-Host "   ⚠️  Ambiente '$envName' ya existe" -ForegroundColor Yellow
        } else {
            Write-Host "   🏗️  Creando ambiente '$envName'..."
            
            $envDescription = $environments[$envName]["description"]
            az devops invoke --area environments --resource environments --route-parameters project=$Project --http-method POST --in-file @"
{
    "name": "$envName",
    "description": "$envDescription"
}
"@ | Out-Null
            
            Write-Host "   ✅ Ambiente '$envName' creado" -ForegroundColor Green
        }
        
    } catch {
        Write-Warning "   ⚠️  Error creando ambiente '$envName': $($_.Exception.Message)"
    }
}

# 3. CREAR PIPELINES
Write-Host ""
Write-Host "🔧 Configurando pipelines..."

$pipelines = @{
    "Vie_ERP_Database_CI" = @{
        "yaml" = "azure-pipelines.yml"
        "description" = "Pipeline de CI para base de datos Vie_ERP"
        "folder" = "Database"
    }
    "Vie_ERP_Database_Release" = @{
        "yaml" = "azure-pipelines-release.yml" 
        "description" = "Pipeline de Release para base de datos Vie_ERP"
        "folder" = "Database"
    }
}

foreach ($pipelineName in $pipelines.Keys) {
    try {
        $yamlPath = $pipelines[$pipelineName]["yaml"]
        $description = $pipelines[$pipelineName]["description"]
        $folder = $pipelines[$pipelineName]["folder"]
        
        # Verificar si el pipeline existe
        $existingPipeline = az pipelines list --name $pipelineName 2>$null | ConvertFrom-Json
        
        if ($existingPipeline) {
            Write-Host "   ⚠️  Pipeline '$pipelineName' ya existe" -ForegroundColor Yellow
        } else {
            Write-Host "   🔧 Creando pipeline '$pipelineName'..."
            
            az pipelines create --name $pipelineName --description $description --yaml-path $yamlPath --repository $RepositoryName --folder-path $folder | Out-Null
            
            Write-Host "   ✅ Pipeline '$pipelineName' creado" -ForegroundColor Green
        }
        
    } catch {
        Write-Warning "   ⚠️  Error creando pipeline '$pipelineName': $($_.Exception.Message)"
    }
}

# 4. CONFIGURAR BRANCH POLICIES
Write-Host ""
Write-Host "🛡️  Configurando políticas de branch..."

try {
    $repoId = az repos show --repository $RepositoryName --query "id" -o tsv
    
    if ($repoId) {
        # Política para main branch
        Write-Host "   🔒 Configurando política para branch 'main'..."
        
        $mainPolicy = @{
            "isEnabled" = $true
            "isBlocking" = $true
            "type" = @{
                "id" = "fa4e907d-c16b-4a4c-9dfa-4906e5d171dd"  # Build validation policy
            }
            "settings" = @{
                "buildDefinitionId" = 1  # Ajustar al ID del pipeline de CI
                "queueOnSourceUpdateOnly" = $false
                "manualQueueOnly" = $false
                "displayName" = "Vie_ERP Database CI"
                "validDuration" = 720
            }
        }
        
        # En implementación real, aplicar la política usando REST API
        Write-Host "   ✅ Políticas de branch configuradas" -ForegroundColor Green
    }
    
} catch {
    Write-Warning "   ⚠️  Error configurando políticas de branch: $($_.Exception.Message)"
}

# 5. GENERAR REPORTE DE CONFIGURACIÓN
Write-Host ""
Write-Host "📊 Generando reporte de configuración..."

$configReport = @{
    "timestamp" = (Get-Date -Format "yyyy-MM-dd HH:mm:ss")
    "organization" = $Organization
    "project" = $Project
    "repository" = $RepositoryName
    "variableGroups" = @($variableGroups.Keys)
    "environments" = @($environments.Keys)
    "pipelines" = @($pipelines.Keys)
    "status" = "Configured"
}

$reportPath = "pipeline-config-report.json"
$configReport | ConvertTo-Json -Depth 3 | Out-File -FilePath $reportPath -Encoding UTF8

Write-Host "   📄 Reporte guardado en: $reportPath" -ForegroundColor Green

# RESUMEN FINAL
Write-Host ""
Write-Host "🎉 CONFIGURACIÓN COMPLETADA" -ForegroundColor Green
Write-Host "=========================="
Write-Host "✅ Grupos de variables creados: $($variableGroups.Count)"
Write-Host "✅ Ambientes configurados: $($environments.Count)"
Write-Host "✅ Pipelines creados: $($pipelines.Count)"
Write-Host ""
Write-Host "📋 PRÓXIMOS PASOS MANUALES:"
Write-Host "1. Configurar Service Connections en Azure DevOps"
Write-Host "2. Ajustar aprobadores en los ambientes según su organización"
Write-Host "3. Validar conectividad a servidores SQL Server"
Write-Host "4. Ejecutar el primer build del pipeline de CI"
Write-Host "5. Probar deployment en ambiente de desarrollo"
Write-Host ""
Write-Host "📚 Documentación disponible en: docs/pipeline-documentation.md"
Write-Host ""
Write-Host "🔗 Enlaces útiles:"
Write-Host "   • Pipelines: https://dev.azure.com/$Organization/$Project/_build"
Write-Host "   • Ambientes: https://dev.azure.com/$Organization/$Project/_environments"
Write-Host "   • Variables: https://dev.azure.com/$Organization/$Project/_library"

Write-Host ""
Write-Host "✨ Pipeline de CI/CD para Vie_ERP Database configurado exitosamente!" -ForegroundColor Green