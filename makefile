all:
	@echo "Starting Watch"
	sass --watch Client/Resources/Styles/main.scss:Client/Resources/Styles/main.css --style compressed
