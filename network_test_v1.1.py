import speedtest
import matplotlib.pyplot as plt


# Função para realizar o teste de velocidade
def testar_velocidade():
    st = speedtest.Speedtest()

    # Buscando o melhor servidor
    print("Buscando o melhor servidor...")
    best_server = st.get_best_server()

    # Exibindo informações do servidor
    server_name = best_server['name']
    server_location = best_server['country']
    ping = best_server['latency']
    print(f"Servidor: {server_name}, Localização: {server_location}, Ping: {ping} ms")

    print("Testando o download...")
    download_speed = st.download()
    print(f"Velocidade de download: {download_speed / 1_000_000:.2f} Mbps")

    print("Testando o upload...")
    upload_speed = st.upload()
    print(f"Velocidade de upload: {upload_speed / 1_000_000:.2f} Mbps")

    return download_speed / 1_000_000, upload_speed / 1_000_000  # Retorna em Mbps

# Executar o teste
download, upload = testar_velocidade()

# Criar gráficos
labels = ['Download', 'Upload']
speeds = [download, upload]

# Definindo cores com base nas velocidades
colors = []
for speed in speeds:
    if speed < 50:
        colors.append('red')
    elif speed < 120:
        colors.append('yellow')
    else:
        colors.append('green')

plt.bar(labels, speeds, color=colors)
plt.ylabel('Velocidade (Mbps)')
plt.title('Teste de Velocidade da Internet')
plt.ylim(0, max(speeds) * 1.2)  # Ajusta o limite do eixo y
plt.grid(axis='y')

# Exibir gráfico
plt.show(block=False)

plt.pause(2)  # Esperar 2 segundos
plt.close()  # Fechar a figura
