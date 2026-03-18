param (
    [string]$Msg = "/start",
    [string]$From = "123456789",
    [string]$Url = "http://localhost:5273/webhook"
)

$Body = @{
    object = "whatsapp_business_account"
    entry = @(
        @{
            id = "0"
            changes = @(
                @{
                    value = @{
                        messaging_product = "whatsapp"
                        metadata = @{
                            display_phone_number = "15550800662"
                            phone_number_id = "100000000000000"
                        }
                        messages = @(
                            @{
                                from = $From
                                id = "mock_msg_id_$(Get-Random)"
                                timestamp = "1665511820"
                                text = @{
                                    body = $Msg
                                }
                                type = "text"
                            }
                        )
                    }
                    field = "messages"
                }
            )
        }
    )
} | ConvertTo-Json -Depth 10

Invoke-RestMethod -Uri $Url -Method Post -Body $Body -ContentType "application/json"
Write-Host "Sent message '$Msg' from '$From' to $Url" -ForegroundColor Green
