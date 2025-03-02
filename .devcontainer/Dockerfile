FROM mcr.microsoft.com/devcontainers/dotnet:1-9.0-bookworm

RUN curl -sSL https://aka.ms/downloadazcopy-v10-linux -o azcopy-netcore_linux_x64.tar.gz \
    && mkdir azcopy \
    && tar xf azcopy-netcore_linux_x64.tar.gz -C azcopy --strip-components 1 \
    && mv azcopy/azcopy /usr/local/bin/azcopy \
    && chmod a+x /usr/local/bin/azcopy \
    && rm -f azcopy-netcore_linux_x64.tar.gz && rm -rf azcopy