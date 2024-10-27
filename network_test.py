import speedtest
import matplotlib.pyplot as plt
from tqdm import tqdm
import time
from datetime import datetime

# Função para realizar o teste de velocidade
def testar_velocidade():
    st = speedtest.Speedtest(secure=True)

    # Buscando o melhor servidor
    print("Buscando o melhor servidor...")
    best_server = st.get_best_server()

    # Exibindo informações do servidor
    server_name = best_server['name']
    server_location = best_server['country']
    ping = best_server['latency']
    print(f"Servidor: {server_name}, Localização: {server_location}, Ping: {ping} ms")

    # Testando o download
    print("Testando o download...")
    download_progress = tqdm(total=100, desc='Download', leave=False)

    download_speed = st.download() / 1_000_000  # Teste real
    download_progress.update(100)  # Atualiza a barra de progresso para 100%
    download_progress.close()
    
    print(f"Velocidade de download: {download_speed:.2f} Mbps")

    # Testando o upload
    print("Testando o upload...")
    upload_progress = tqdm(total=100, desc='Upload', leave=False)

    upload_speed = st.upload() / 1_000_000  # Teste real
    upload_progress.update(100)  # Atualiza a barra de progresso para 100%
    upload_progress.close()

    print(f"Velocidade de upload: {upload_speed:.2f} Mbps")

    return server_name, server_location, ping, download_speed, upload_speed  # Retorna as informações

# Função para registrar os dados em um arquivo de log
def registrar_log(server_name, server_location, ping, download_speed, upload_speed):
    log_filename = "log.txt"
    timestamp = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    with open(log_filename, 'a') as log_file:
        log_file.write(f"{timestamp} - Servidor: {server_name}, Localização: {server_location}, "
                       f"Ping: {ping} ms, Download: {download_speed:.2f} Mbps, "
                       f"Upload: {upload_speed:.2f} Mbps\n")

# Loop para executar o teste a cada 15 segundos
while True:
    # Contagem regressiva
    for i in range(15, 0, -1):
        print(f"Próximo teste em {i} segundos...", end='\r')
        time.sleep(1)
    
    print("Iniciando o teste...\n")  # Nova linha após a contagem

    # Realizar o teste
    server_name, server_location, ping, download, upload = testar_velocidade()

    # Registrar no log
    registrar_log(server_name, server_location, ping, download, upload)

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

