import speedtest

st = speedtest.Speedtest()

# Buscando servidores disponíveis
print("Buscando servidores disponíveis...")
servers = st.get_servers()

# Exibindo servidores encontrados
server_list = []
for server in servers.values():
    for s in server:
        server_list.append(s)

# Exibindo os servidores disponíveis
print("Servidores encontrados:")
for s in server_list:
    print(f"ID: {s['id']}, Nome: {s['name']}, Localização: {s['country']}")

# Substitua 'ID_DO_SERVIDOR' pelo ID do servidor que você deseja testar
selected_server_id = input("Digite o ID do servidor que você deseja testar: ")

# Verifica se o servidor está na lista
selected_server = next((s for s in server_list if str(s['id']) == selected_server_id), None)

if selected_server:
    print(f"Servidor selecionado: {selected_server['name']}, Localização: {selected_server['country']}")

    # Configurando o servidor selecionado
    st._server = selected_server  # Usar um método interno para definir o servidor

    # Testando o download no servidor selecionado
    print("Testando o download...")
    download_speed = st.download()
    print(f"Velocidade de download: {download_speed / 1_000_000:.2f} Mbps")

    # Testando o upload no servidor selecionado
    print("Testando o upload...")
    upload_speed = st.upload()
    print(f"Velocidade de upload: {upload_speed / 1_000_000:.2f} Mbps")
else:
    print("Servidor não encontrado. Verifique se o ID está correto.")
